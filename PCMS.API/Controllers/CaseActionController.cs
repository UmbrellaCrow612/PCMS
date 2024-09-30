using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PCMS.API.BusinessLogic;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("cases/{caseId}/actions")]
    [Authorize]
    public class CaseActionController(ApplicationDbContext context, IMapper mapper, ICaseActionService caseActionService) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
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

        /// <summary>
        /// Retrieves a specific case action by its ID.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the case action.</param>
        /// <returns>The requested case action.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GETCaseAction>> GetAction(string caseId, string id)
        {
            var existingCaseAction = await _context.CaseActions
                .Where(ca => ca.CaseId == caseId && ca.Id == id)
                .FirstOrDefaultAsync();

            if (existingCaseAction is null)
            {
                return NotFound("Case action not found or not associated with the specified case");
            }

            var returnCaseAction = _mapper.Map<GETCaseAction>(existingCaseAction);

            return Ok(returnCaseAction);
        }

        /// <summary>
        /// Retrieves all case actions related to a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>A list of all case actions for the specified case.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETCaseAction>>> GetActions(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case does not exist");
            }

            var existingCaseActions = await _context.CaseActions
                .Where(ca => ca.CaseId == caseId)
                .ToListAsync();

            var returnCaseActions = _mapper.Map<List<GETCaseAction>>(existingCaseActions);

            return Ok(returnCaseActions);
        }

        /// <summary>
        /// Updates a case action by its ID.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the case action to update.</param>
        /// <param name="request">The DTO containing the updated case action information.</param>
        /// <returns>No content if successful.</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<IActionResult> PatchCaseAction(string caseId, string id, [FromBody] PATCHCaseAction request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var caseAction = await _context.CaseActions.FirstOrDefaultAsync(ca => ca.Id == id && ca.CaseId == caseId);
            if (caseAction is null)
            {
                return NotFound("Case action does not exist or is not linked to the specified case");
            }

            caseAction.Name = request.Name;
            caseAction.Description = request.Description;
            caseAction.Type = request.Type;
            caseAction.LastEditedById = userId;
            caseAction.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a case action by its ID.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the case action to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCaseAction(string caseId, string id)
        {
            var caseAction = await _context.CaseActions.FirstOrDefaultAsync(ca => ca.Id == id && ca.CaseId == caseId);
            if (caseAction is null)
            {
                return NotFound("Case action does not exist or is not linked to the specified case");
            }

            _context.CaseActions.Remove(caseAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}