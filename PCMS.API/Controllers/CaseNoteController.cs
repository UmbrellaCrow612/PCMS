using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("/cases/{caseId}/case-notes")]
    [Authorize]
    public class CaseNoteController(ICaseNoteService caseNoteService) : ControllerBase
    {
        private readonly ICaseNoteService _caseNoteService = caseNoteService;

        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CaseNoteDto>> CreateCaseNote(string caseId, [FromBody] CreateCaseNoteDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var caseNote = await _caseNoteService.CreateCaseNoteAsync(caseId, userId, request);
            if (caseNote is null)
            {
                return NotFound("Case not found.");
            }

            return Created(nameof(CreateCaseNote), caseNote);
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CaseNoteDto>>> GetCaseNotes(string caseId)
        {
            var caseNotes = await _caseNoteService.GetCaseNotesForCaseIdAsync(caseId);
            if (caseNotes is null)
            {
                return NotFound("Case not found");
            }

            return Ok(caseNotes);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CaseNoteDto>> GetCaseNote(string caseId, string id)
        {
            var caseNote = await _caseNoteService.GetCaseNoteByIdAsync(id, caseId);
            if(caseNote is null)
            {
                return NotFound("Case or case note not found.");
            }

            return Ok(caseNote);
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PatchCaseNote(string caseId, string id, [FromBody] UpdateCaseNoteDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var updatedCaseNote = await _caseNoteService.UpdateCaseNoteByIdAsync(id, caseId, userId, request);
            if (updatedCaseNote is null)
            {
                return NotFound("Case or case note not found.");
            }

            return Ok(updatedCaseNote);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCaseNote(string caseId, string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var isDeleted = await _caseNoteService.DeleteCaseNoteByIdAsync(id, caseId, userId);
            if (!isDeleted)
            {
                return NotFound("Case or case note not found.");
            }

            return NoContent();
        }
    }
}