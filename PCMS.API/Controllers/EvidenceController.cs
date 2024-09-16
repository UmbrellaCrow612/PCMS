using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling evidence-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EvidenceController"/> class.
    /// </remarks>
    /// <param name="context">The database context.</param>
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
        /// <returns>The created evidence item.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETEvidence>> CreateEvidence(string caseId, POSTEvidence request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (!await _context.Cases.AnyAsync(c => c.Id == caseId))
            {
                return NotFound("Case not found");
            }

            var evidence = _mapper.Map<Evidence>(request, opts =>
            {
                opts.AfterMap((src, dest) =>
                {
                    dest.CaseId = caseId;
                    dest.LastEditedById = userId;
                    dest.CreatedById = userId;
                });
            });

            await _context.Evidences.AddAsync(evidence);
            await _context.SaveChangesAsync();

            var returnEvidence = _mapper.Map<GETEvidence>(evidence);
            return CreatedAtAction(nameof(CreateEvidence), new { id = returnEvidence.Id }, returnEvidence);
        }

        /// <summary>
        /// Gets all evidence items for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>The list of evidence items.</returns>
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETEvidence>>> GetEvidences(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found");
            }

            var evidences = await _context.Evidences
                .Where(e => e.CaseId == caseId)
                .ProjectTo<GETEvidence>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(evidences);
        }


        /// <summary>
        /// Get a evidence item for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the evidence</param>
        /// <returns>The evidence item.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETEvidence>> GetEvidence(string caseId, string id)
        {
            var evidence = await _context.Evidences
                .Where(e => e.Id == id && e.CaseId == caseId)
                .ProjectTo<GETEvidence>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (evidence is null)
            {
                return NotFound("Evidence not found or is not linked to this case");
            }

            return Ok(evidence);
        }

        /// <summary>
        /// Patch a evidence item for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the evidence</param>
        /// <param name="request">The DTO PATCH data for the evidence item.</param>
        /// <returns>No Content.</returns>
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchEvidence(string caseId, string id, [FromBody] PATCHEvidence request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var evidence = await _context.Evidences.Where(e => e.CaseId == caseId && e.Id == id).FirstOrDefaultAsync();
            if (evidence is null)
            {
                return NotFound("Evidence not found or is not linked to this case.");
            }

            _mapper.Map(request, evidence);

            evidence.LastEditedById = userId;
            evidence.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete a evidence item for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the evidence</param>
        /// <returns>No Content.</returns>
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEvidence(string caseId, string id)
        {
            var evidence = await _context.Evidences.Where(e => e.CaseId == caseId && e.Id == id).FirstOrDefaultAsync();
            if (evidence is null)
            {
                return NotFound("Case not found or is linked to this case");
            }

            _context.Remove(evidence);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}