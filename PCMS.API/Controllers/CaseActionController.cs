using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

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
    public class CaseActionController(ILogger<CaseActionController> logger, ApplicationDbContext context) : ControllerBase
    {
        private readonly ILogger<CaseActionController> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Creates a new case action for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="request">The DTO containing POST case action information.</param>
        /// <returns>The created case action details.</returns>
        /// <response code="201">Returns the newly created case action.</response>
        /// <response code="400">Returns when the request is invalid.</response>
        /// <response code="404">Returns when the case or user is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GETCaseAction), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETCaseAction>> CreateAction([FromRoute][Required] string caseId, [FromBody] POSTCaseAction request)
        {
            try
            {
                _logger.LogInformation("POST case action request received for case ID: {CaseId}", caseId);

                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Case ID cannot be null or empty");
                }

                var userExists = await _context.Users.AnyAsync(u => u.Id == request.CreatedById);
                if (!userExists)
                {
                    return NotFound("User does not exist");
                }

                var case_ = await _context.Cases.FirstOrDefaultAsync(c => c.Id == caseId);
                if (case_ is null)
                {
                    return NotFound("Case does not exist");
                }

                var caseAction = new CaseAction
                {
                    Name = request.Name,
                    Description = request.Description,
                    Type = request.Type,
                    Case = case_,
                    CaseId = caseId,
                    CreatedById = request.CreatedById,
                    LastEditedById = request.CreatedById,
                };

                await _context.CaseActions.AddAsync(caseAction);
                await _context.SaveChangesAsync();

                var createdCaseAction = new GETCaseAction
                {
                    Id = caseAction.Id,
                    Name = caseAction.Name,
                    Description = caseAction.Description,
                    Type = caseAction.Type,
                    CreatedAt = caseAction.CreatedAt,
                    LastModifiedDate = caseAction.LastModifiedDate,
                    CreatedById = caseAction.CreatedById,
                    LastEditedById = caseAction.LastEditedById
                };

                _logger.LogInformation("Created a new case action with ID: {CaseActionId} for case ID: {CaseId}", createdCaseAction.Id, caseId);

                return CreatedAtAction(nameof(GetAction), new { caseId, id = createdCaseAction.Id }, createdCaseAction);
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
        /// <response code="400">Returns when the ID is invalid.</response>
        /// <response code="404">Returns when the case action is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GETCaseAction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETCaseAction>> GetAction([FromRoute][Required] string caseId, [FromRoute][Required] string id)
        {
            try
            {
                _logger.LogInformation("Get case action request received for case ID: {CaseId}, action ID: {Id}", caseId, id);

                if (string.IsNullOrEmpty(caseId) || string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID or case action ID cannot be null or empty");
                }

                var caseAction = await _context.CaseActions
                    .Where(ca => ca.CaseId == caseId && ca.Id == id)
                    .Select(ca => new GETCaseAction
                    {
                        Id = ca.Id,
                        Name = ca.Name,
                        Description = ca.Description,
                        Type = ca.Type,
                        CreatedAt = ca.CreatedAt,
                        LastModifiedDate = ca.LastModifiedDate,
                        CreatedById = ca.CreatedById,
                        LastEditedById = ca.LastEditedById
                    })
                    .FirstOrDefaultAsync();

                if (caseAction is null)
                {
                    return NotFound("Case action not found or not associated with the specified case");
                }

                _logger.LogInformation("Get case action request for case ID: {CaseId}, action ID: {Id} successful", caseId, id);

                return Ok(caseAction);
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
        /// <response code="404">Returns when the case is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GETCaseAction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GETCaseAction>>> GetActions([FromRoute][Required] string caseId)
        {
            try
            {
                _logger.LogInformation("Get request received for all case actions of case ID: {CaseId}", caseId);

                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Case ID cannot be null or empty");
                }

                var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
                if (!caseExists)
                {
                    return NotFound("Case does not exist");
                }

                var caseActions = await _context.CaseActions
                    .Where(ca => ca.CaseId == caseId)
                    .Select(ca => new GETCaseAction
                    {
                        Id = ca.Id,
                        Name = ca.Name,
                        Description = ca.Description,
                        Type = ca.Type,
                        CreatedAt = ca.CreatedAt,
                        LastModifiedDate = ca.LastModifiedDate,
                        CreatedById = ca.CreatedById,
                        LastEditedById = ca.LastEditedById,
                      
                    })
                    .ToListAsync();

                return Ok(caseActions);
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
        /// <response code="404">Returns when the case action is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchCaseAction([FromRoute][Required] string caseId, [FromRoute][Required] string id, [FromBody] PATCHCaseAction request)
        {
            try
            {
                _logger.LogInformation("Patch request received for case action. Case ID: {CaseId}, Action ID: {Id}", caseId, id);

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
                caseAction.LastEditedById = request.LastEditedById;
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
        /// <response code="404">Returns when the case action is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCaseAction([FromRoute][Required] string caseId, [FromRoute][Required] string id)
        {
            try
            {
                _logger.LogInformation("Delete request received for case action. Case ID: {CaseId}, Action ID: {Id}", caseId, id);

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