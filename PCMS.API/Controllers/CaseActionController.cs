using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case action related operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CaseActionController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    [ApiController]
    [Route("cases/{caseId}/actions")]
    [Produces("application/json")]
    [Authorize]
    public class CaseActionController(ILogger<CaseActionController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<CaseActionController> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new case action for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="request">The DTO containing POST case action information.</param>
        /// <returns>The created case action details.</returns>
        /// <response code="201">Returns the newly created case action.</response>
        /// <response code="400">Returns when the request is invalid.</response>
        /// <response code="401">Returns when the user is unauthorized.</response>
        /// <response code="404">Returns when the case or user is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GETCaseAction), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETCaseAction>> CreateAction([FromRoute][Required] string caseId, [FromBody] POSTCaseAction request)
        {
            _logger.LogInformation("POST case action request received for case ID: {CaseId}", caseId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Unauthorized");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Unauthorized("Unauthorized");
            }

            try
            {
                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Case ID cannot be null or empty");
                }

                var existingCase = await _context.Cases.FindAsync(caseId);
                if (existingCase is null)
                {
                    return NotFound("Case does not exist");
                }

                var caseAction = _mapper.Map<CaseAction>(request);
                caseAction.CaseId = caseId;
                caseAction.CreatedById = userId;
                caseAction.LastEditedById = userId;

                await _context.CaseActions.AddAsync(caseAction);
                await _context.SaveChangesAsync();

                var returnCaseAction = _mapper.Map<GETCaseAction>(caseAction);

                _logger.LogInformation("Created a new case action with ID: {CaseActionId} for case ID: {CaseId}", returnCaseAction.Id, caseId);

                return CreatedAtAction(nameof(CreateAction), new { caseId, id = returnCaseAction.Id }, returnCaseAction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a new case action. CaseId: {CaseId}, Request: {@Request}", caseId, request);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a specific case action by its ID.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the case action.</param>
        /// <returns>The requested case action.</returns>
        /// <response code="200">Returns the requested case action.</response>
        /// <response code="401">Returns when the user is unauthorized.</response>
        /// <response code="400">Returns when the ID is invalid.</response>
        /// <response code="404">Returns when the case action is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GETCaseAction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETCaseAction>> GetAction([FromRoute][Required] string caseId, [FromRoute][Required] string id)
        {
            _logger.LogInformation("Get case action request received for case ID: {CaseId}, action ID: {Id}", caseId, id);

            try
            {
                if (string.IsNullOrEmpty(caseId) || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID or case action ID cannot be null or empty");
                }

                var existingCaseAction = await _context.CaseActions
                    .Where(ca => ca.CaseId == caseId && ca.Id == id)
                    .FirstOrDefaultAsync();

                if (existingCaseAction is null)
                {
                    return NotFound("Case action not found or not associated with the specified case");
                }

                var returnCaseAction = _mapper.Map<GETCaseAction>(existingCaseAction);

                _logger.LogInformation("Get case action request for case ID: {CaseId}, action ID: {Id} successful", caseId, id);

                return Ok(returnCaseAction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get a case action. CaseId: {CaseId}, ActionId: {Id}", caseId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves all case actions related to a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>A list of all case actions for the specified case.</returns>
        /// <response code="200">Returns the list of all case actions for the specified case.</response>
        /// <response code="400">Returns when the case ID is invalid.</response>
        /// <response code="401">Returns when the case user is unauthorized.</response>
        /// <response code="404">Returns when the case is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GETCaseAction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GETCaseAction>>> GetActions([FromRoute][Required] string caseId)
        {
            _logger.LogInformation("Get request received for all case actions of case ID: {CaseId}", caseId);

            try
            {
                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Case ID cannot be null or empty");
                }

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get case actions for case ID: {CaseId}", caseId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a case action by its ID.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the case action to update.</param>
        /// <param name="request">The DTO containing the updated case action information.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Returns when the case action is successfully updated.</response>
        /// <response code="400">Returns when the request is invalid.</response>
        /// <response code="401">Returns when the case user is unauthorized.</response>
        /// <response code="404">Returns when the case action is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchCaseAction([FromRoute][Required] string caseId, [FromRoute][Required] string id, [FromBody] PATCHCaseAction request)
        {
            _logger.LogInformation("Patch request received for case action. Case ID: {CaseId}, Action ID: {Id}", caseId, id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Unauthorized");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Unauthorized("Unauthorized");
            }

            try
            {
                if (string.IsNullOrEmpty(caseId) || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID or case action ID cannot be null or empty");
                }

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

                _logger.LogInformation("Patch request for case action. Case ID: {CaseId}, Action ID: {Id} is successful", caseId, id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to patch a case action. CaseId: {CaseId}, ActionId: {Id}", caseId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a case action by its ID.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="id">The ID of the case action to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Returns when the case action is successfully deleted.</response>
        /// <response code="400">Returns when the ID is invalid.</response>
        /// <response code="401">Returns when the case user is unauthorized.</response>
        /// <response code="404">Returns when the case action is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCaseAction([FromRoute][Required] string caseId, [FromRoute][Required] string id)
        {
            _logger.LogInformation("Delete request received for case action. Case ID: {CaseId}, Action ID: {Id}", caseId, id);

            try
            {
                if (string.IsNullOrEmpty(caseId) || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID or case action ID cannot be null or empty");
                }

                var caseAction = await _context.CaseActions.FirstOrDefaultAsync(ca => ca.Id == id && ca.CaseId == caseId);
                if (caseAction is null)
                {
                    return NotFound("Case action does not exist or is not linked to the specified case");
                }

                _context.CaseActions.Remove(caseAction);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Delete request for case action. Case ID: {CaseId}, Action ID: {Id} successful", caseId, id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete a case action. CaseId: {CaseId}, ActionId: {Id}", caseId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}