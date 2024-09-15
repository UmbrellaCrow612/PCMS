using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling evidence-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EvidenceController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    /// <param name="userManager">Identity service to manage users</param>
    /// <param name="mapper">Automapper instance</param>
    [ApiController]
    [Route("cases/{caseId}/evidences")]
    [Authorize]
    public class EvidenceController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new evidence item for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="request">The DTO containing POST evidence information.</param>
        /// <returns>The created evidence details.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETEvidence>> CreateEvidence([FromRoute] string caseId, POSTEvidence request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found");
            }

            var evidence = _mapper.Map<Evidence>(request);
            evidence.CaseId = caseId;
            evidence.LastEditedById = userId;
            evidence.CreatedById = userId;

            await _context.Evidences.AddAsync(evidence);

            await _context.SaveChangesAsync();

            var returnEvidence = _mapper.Map<GETEvidence>(evidence);

            return CreatedAtAction(nameof(CreateEvidence), new { id = returnEvidence.Id }, returnEvidence);

        }
    }
}