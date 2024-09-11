using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Models;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case action related actions.
    /// </summary>
    [ApiController]
    [Route("/cases/{caseId}/actions")]
    public class CaseActionController(ILogger<CaseController> logger, ApplicationDbContext context) : ControllerBase
    {

        private readonly ILogger<CaseController> _logger = logger;
        private readonly ApplicationDbContext _context = context;


        /// <summary>
        /// Create a new case action for a specific case
        /// </summary>
        /// <param name="request">The DTO containing POST case action information.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAction(string caseId, [FromBody] POSTCaseAction request)
        {
            try
            {
                _logger.LogInformation("Received POST request for case action for case Id: {caseId}", caseId);

                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Null or empty caseId");
                }

                var _userExists = await _context.Users.AnyAsync(u => u.Id == request.CreatedById);

                if (_userExists is false)
                {
                    return NotFound("User dose not exist");
                }

                var _case = await _context.Cases.FirstOrDefaultAsync(c => c.Id == caseId);

                if (_case is null)
                {
                    return NotFound("Case dose not exist");
                }

                var _case_action = new CaseAction
                {
                    Name = request.Name,
                    Description = request.Description,
                    Type = request.Type,
                    Case = _case,
                    CaseId = caseId,
                    CreatedById = request.CreatedById,
                };

                await _context.AddAsync(_case_action);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateAction), new { id = _case_action.Id });

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create a new case action, details: request: {request} ex: {ex}", request, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Get all case actions related to a case based on caseId.
        /// </summary>
        /// <returns>A response indicating success or failure.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActions(string caseId)
        {
            try
            {
                _logger.LogInformation("Received GET request for case action for case Id: {caseId}", caseId);

                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Null or empty caseId");
                }

                var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);

                if (caseExists is false)
                {
                    return NotFound("Case dose not exist");
                }

                var _case_actions = await _context.Cases
                            .Where(c => c.Id == caseId)
                            .SelectMany(c => c.CaseActions)
                            .Select(ca => new GETCaseAction
                            {
                                Id = ca.Id,
                                Name = ca.Name,
                                Description = ca.Description,
                                Type = ca.Type,
                                CreatedAt = ca.CreatedAt,
                                CreatedById = ca.CreatedById,
                            })
                            .ToListAsync();

                return Ok(_case_actions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get case actions : caseId: {caseId} ex: {ex}", caseId, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Update a case action related to a case based on caseId and case action ID.
        /// </summary>
        /// <param name="request">DTO for updating a case action</param>
        /// <param name="caseId">The case Id of the case</param>
        /// <param name="id">The Id of the case action</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchCaseAction(string caseId, string id, PATCHCaseAction request)
        {
            try
            {
                _logger.LogInformation("Received PATCH request for case action for case Id: {caseId} and case action Id: {id}", caseId, id);

                if (string.IsNullOrEmpty(caseId) | string.IsNullOrEmpty(id))
                {
                    return BadRequest("case Id or case action Id is null or empty");
                }

                var _caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
                var _caseActionExists = await _context.CaseActions.AnyAsync(ca => ca.Id == id);

                if (_caseExists is false)
                {
                    return NotFound("Case dose not exist");
                }

                if (_caseActionExists is false)
                {
                    return NotFound("Case action dose not exist");
                }

                var _case_action = await _context.CaseActions.FirstOrDefaultAsync(ca => ca.Id == id && ca.CaseId == caseId);

                if (_case_action is null)
                {
                    return NotFound("Case action does not exist or is not linked to the specified case.");
                }

                _case_action.Name = request.Name;
                _case_action.Description = request.Description;
                _case_action.Type = request.Type;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to PATCH case action : caseId: {caseId} case action Id: {id} ex: {ex}", caseId, id, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}