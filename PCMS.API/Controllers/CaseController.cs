using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.BusinessLogic;
using PCMS.API.Dtos.Read;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.PATCH;
using PCMS.API.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PCMS.API.Dtos.Create;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case-related actions.
    /// </summary>
    [ApiController]
    [Route("cases")]
    [Authorize]
    public class CaseController(ICaseService caseService) : ControllerBase
    {
        private readonly ICaseService _caseService = caseService;

        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CaseDto>> CreateCase([FromBody] CreateCaseDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var _case = await _caseService.CreateCaseAsync(request, userId);

            return Created(nameof(CreateCase), _case);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CaseDto>> GetCase(string id)
        {
            var result = await _caseService.GetCaseByIdAsync(id);

            if (result is null)
            {
                return NotFound("Case not found");
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<ActionResult> PatchCase(string id, [FromBody] UpdateCaseDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var isUpdated = await _caseService.UpdateCaseByIdAsync(id, userId, request);

            if (!isUpdated)
            {
                return NotFound("Case not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCase(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var isDeleted = await _caseService.DeleteCaseByIdAsync(id, userId);
            if (!isDeleted)
            {
                return NotFound("Case to delete not found");
            }

            return NoContent();
        }

        [HttpGet("{id}/persons")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PersonDto>>> GetCasePersons(string id)
        {
            var persons = await _caseService.GetLinkedPersonsForCaseIdAsync(id);

            return Ok(persons);
        }

        [HttpGet("{id}/users")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ApplicationUserDto>>> GetUserCases(string id)
        {
            var users = await _caseService.GetLinkedUsersForCaseIdAsync(id);

            return Ok(users);
        }

        [HttpGet("{id}/edits")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CaseEditDto>>> GetCaseEdits(string id)
        {
            var edits = await _caseService.GetCaseEditsForCaseIdAsync(id);

            return Ok(edits);
        }

        [HttpGet("{id}/tags")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TagDto>>> GetCaseTags(string id)
        {
            var tags = await _caseService.GetLinkedTagsForCaseIdAsync(id);

            return Ok(tags);
        }
    }
}