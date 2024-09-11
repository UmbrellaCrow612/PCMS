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

        // TODO: Implement other action methods as per the convention
        // GET    /cases/{caseId}/actions
        // GET    /cases/{caseId}/actions/{actionId}
        // PATCH  /cases/{caseId}/actions/{actionId}
        // DELETE /cases/{caseId}/actions/{actionId}
    }
}