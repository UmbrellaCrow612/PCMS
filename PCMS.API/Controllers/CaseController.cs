using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Models;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case related actions.
    /// </summary>
    [ApiController]
    [Route("cases")]
    public class CaseController(ILogger<CaseController> logger, ApplicationDbContext context) : ControllerBase
    {
        private readonly ILogger<CaseController> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Create a new case
        /// </summary>
        /// <param name="request">The DTO containing POST case information.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] POSTCase request)
        {
            try
            {
                _logger.LogInformation("POST case request received");

                var _user = await _context.Users.FirstOrDefaultAsync(e => e.Id == request.CreatedById);

                if (_user is null)
                {
                    _logger.LogInformation("POST case user not found");

                    return BadRequest("User dose not exist");
                }

                var _case = new Case
                {
                    CaseNumber = "1234567",
                    Title = request.Title,
                    Description = request.Description,
                    Priority = request.Priority,
                    Type = request.Type,
                    CreatedBy = _user,
                    CreatedById = request.CreatedById,
                    LastModifiedBy = _user,
                    LastModifiedById = request.CreatedById
                };

                _logger.LogInformation("POST case object {Case} created, attempting to save", _case);

                await _context.Cases.AddAsync(_case);

                _logger.LogInformation("Added a case into the Database");

                await _context.SaveChangesAsync();

                _logger.LogInformation("Saved a case into the Database");

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create a new case: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }
}
