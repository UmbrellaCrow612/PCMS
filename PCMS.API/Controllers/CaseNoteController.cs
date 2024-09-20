﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling CaseNote-related actions.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">Auto Mapper</param>
    [ApiController]
    [Route("/cases/{caseId}/case-notes")]
    [Authorize]
    public class CaseNoteController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new case note.
        /// </summary>
        /// <param name="request">The DTO containing POST case note information.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>The created case note details.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETCaseNote>> CreateCaseNote(string caseId, [FromBody] POSTCaseNote request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var caseNote = _mapper.Map<CaseNote>(request);
            caseNote.CreatedById = userId;
            caseNote.CaseId = caseId;

            await _context.CaseNotes.AddAsync(caseNote);
            await _context.SaveChangesAsync();

            var returnCaseNote = _mapper.Map<GETCaseNote>(caseNote);

            return CreatedAtAction(nameof(CreateCaseNote), new { id = returnCaseNote.Id }, returnCaseNote);
        }

        /// <summary>
        /// Gets a list of case notes.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>The list of case note details.</returns>
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETCaseNote>>> GetCaseNotes(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var caseNotes = await _context.CaseNotes.Where(cn => cn.CaseId == caseId).ToListAsync();

            var returnCaseNotes = _mapper.Map<List<GETCaseNote>>(caseNotes);

            return Ok(returnCaseNotes);
        }
    }
}
