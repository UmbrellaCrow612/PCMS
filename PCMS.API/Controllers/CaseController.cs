using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Models;
using System.Net;

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
        public async Task<ActionResult> CreateCase([FromBody] POSTCase request)
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
                    CreatedById = request.CreatedById,
                    LastModifiedById = request.CreatedById
                };

                _logger.LogInformation("POST case object {Case} created, attempting to save", _case);

                await _context.Cases.AddAsync(_case);

                _logger.LogInformation("Added a case into the Database");

                await _context.SaveChangesAsync();

                _logger.LogInformation("Saved a case into the Database");

                return CreatedAtAction(nameof(CreateCase), new { id = _case.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create a new case: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Get a case by Id
        /// </summary>
        /// <param name="id">The Id of the case</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Case), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Case>> GetCase(string id)
        {
            try
            {
                _logger.LogInformation("Get case request received for id: {id}", id);

                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogInformation("Get case request received for id: {id} is null or empty", id);

                    return BadRequest("Case ID cannot be null or empty.");
                }

                var _case = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);

                if (_case is null)
                {
                    _logger.LogInformation("Get case request received for id: {id} not found", id);

                    return NotFound($"Case with ID '{id}' was not found.");
                }

                _logger.LogInformation("Get case request received for id: {id} successful", id);

                return Ok(_case);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get a case: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Get all cases
        /// </summary>
        /// <returns>A response indicating success or failure.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Case), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Case>>> GetCases() 
        {
            try
            {
                _logger.LogInformation("Get request received for all cases.");

                var _cases = await _context.Cases.ToListAsync();

                _logger.LogInformation("Get request received for all cases successful.");

                return Ok(_cases);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get a cases: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        } 

    }
}
