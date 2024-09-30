using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Filters;
using System.Security.Claims;
using PCMS.API.BusinessLogic;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("cases/{caseId}/actions")]
    [Authorize]
    public class CaseActionController(ICaseActionService caseActionService) : ControllerBase
    {
        private readonly ICaseActionService _caseActionService = caseActionService;

        [HttpPost]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETCaseAction>> CreateAction(string caseId, [FromBody] POSTCaseAction request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var caseAction = await _caseActionService.CreateCaseActionAsync(caseId, userId, request);

            if (caseAction is null)
            {
                return NotFound("Case not found.");
            }

            return Created(nameof(CreateAction), caseAction);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GETCaseAction>> GetAction(string caseId, string id)
        {
            var caseAction = await _caseActionService.GetCaseActionByIdAsync(id, caseId);

            if (caseAction is null)
            {
                return NotFound("Case action not found or is linked to this case.");
            }

            return Ok(caseAction);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETCaseAction>>> GetActions(string caseId)
        {
           var caseActions = await _caseActionService.GetCaseActionsForCaseIdAsync(caseId);

            return Ok(caseActions);
        }

        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PatchCaseAction(string caseId, string id, [FromBody] PATCHCaseAction request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var updatedCaseAction = await _caseActionService.UpdateCaseActionByIdAsync(id, caseId, userId, request);

            if (updatedCaseAction is null)
            {
                return NotFound("Case action not found or is linked to this case.");
            }

            return Ok(updatedCaseAction);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCaseAction(string caseId, string id)
        {
           var isDeleted = await _caseActionService.DeleteCaseActionByIdAsync(id, caseId);
            if (!isDeleted)
            {
                return NotFound("Case action not found or is linked to this case.");
            }

            return NoContent();
        }
    }
}