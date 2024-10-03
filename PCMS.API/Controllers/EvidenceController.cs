using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.BusinessLogic;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{

    [ApiController]
    [Route("cases/{caseId}/evidences")]
    [Authorize]
    public class EvidenceController(IEvidenceService evidenceService) : ControllerBase
    {
        private readonly IEvidenceService _evidenceService = evidenceService;

        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EvidenceDto>> CreateEvidence(string caseId, CreateEvidenceDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var evidence = await _evidenceService.CreateEvidenceAsync(caseId,userId,request);

            if (evidence is null)
            {
                return NotFound("Case not found.");
            }

            return Created(nameof(CreateEvidence), evidence);
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EvidenceDto>>> GetEvidences(string caseId)
        {
            var evidences = await _evidenceService.GetEvidenceForCaseIdAsync(caseId);

            if (evidences is null)
            {
                return NotFound("Case not found.");
            }

            return Ok(evidences);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EvidenceDto>> GetEvidence(string caseId, string id)
        {
            var evidence = await _evidenceService.GetEvidenceByIdAsync(id, caseId);

            if (evidence is null)
            {
                return NotFound("Case not found.");
            }

            return Ok(evidence);
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PatchEvidence(string caseId, string id, [FromBody] PATCHEvidence request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var evidence = await _evidenceService.UpdatetEvidenceByIdAsync(id, caseId, userId, request);

            if (evidence is null)
            {
                return NotFound();
            }

            return Ok(evidence);
        
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEvidence(string caseId, string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var isDeleted = await _evidenceService.DeleteEvidenceByIdAsync(id,caseId,userId);
            if (!isDeleted)
            {
                return NotFound("Evidence not found.");
            }

            return NoContent();
        }
    }
}