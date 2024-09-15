using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CaseController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    [ApiController]
    [Route("cases")]
    [Authorize]
    public class CaseController(ILogger<CaseController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<CaseController> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new case.
        /// </summary>
        /// <param name="request">The DTO containing POST case information.</param>
        /// <returns>The created case details.</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        public async Task<ActionResult<GETCase>> CreateCase([FromBody] POSTCase request)
        {
            _logger.LogInformation("POST case request received");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var newCase = _mapper.Map<Case>(request);
                newCase.CreatedById = userId;
                newCase.LastEditedById = userId;

                _context.Cases.Add(newCase);
                await _context.SaveChangesAsync();

                var createdCase = await _context.Cases
                    .FirstOrDefaultAsync(c => c.Id == newCase.Id)
                    ?? throw new ApplicationException("Failed to retrieve the created case");

                var returnCase = _mapper.Map<GETCase>(createdCase);

                _logger.LogInformation("User {UserId} created a new case with ID: {CaseId}", userId, returnCase.Id);

                return CreatedAtAction(nameof(GetCase), new { id = returnCase.Id }, returnCase);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Failed to map POSTCase to Case for request: {@Request} by user {UserId}", request, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating a new case. Request: {@Request} by user {UserId}", request, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



        /// <summary>
        /// Retrieves a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>The requested case.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETCase>> GetCase([FromRoute] string id)
        {
            _logger.LogInformation("GET case request received for ID: {Id}", id);

            try
            {
                var caseEntity = await _context.Cases
                    .Include(c => c.CaseActions)
                    .Include(c => c.AssignedUsers)
                    .Include(c => c.Reports)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (caseEntity is null)
                {
                    _logger.LogWarning("Get case request failed: Case with ID {Id} not found.", id);
                    return NotFound($"Case with ID '{id}' was not found.");
                }

                var caseResult = _mapper.Map<GETCase>(caseEntity);

                _logger.LogInformation("GET case request for ID: {Id} successful. Case found.", id);
                return Ok(caseResult);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Failed to map Case to GETCase for case ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving case with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves all cases.
        /// </summary>
        /// <returns>A list of all cases.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETCase>>> GetCases()
        {
            _logger.LogInformation("GET request received to retrieve all cases.");

            try
            {
                var cases = await _context.Cases
                    .Include(c => c.CaseActions)
                    .Include(c => c.AssignedUsers)
                    .Include(c => c.Reports)
                    .ToListAsync();

                if (cases.Count is 0)
                {
                    _logger.LogInformation("No cases found.");
                    return Ok(new List<GETCase>());
                }

                var returnCases = _mapper.Map<List<GETCase>>(cases);

                _logger.LogInformation("{Count} cases retrieved successfully.", returnCases.Count);

                return Ok(returnCases);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Failed to map Case entities to GETCase DTOs.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving cases.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case to update.</param>
        /// <param name="request">The DTO containing the updated case information.</param>
        /// <returns>No content if successful.</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        public async Task<ActionResult> PatchCase([FromRoute] string id, [FromBody] PATCHCase request)
        {
            _logger.LogInformation("PATCH request received for case ID: {Id}", id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var existingCase = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);
                if (existingCase is null)
                {
                    _logger.LogWarning("PATCH request failed: Case with ID {Id} not found. User ID: {UserId}", id, userId);
                    return NotFound("Case not found.");
                }

                existingCase.Title = request.Title;
                existingCase.Description = request.Description;
                existingCase.Status = request.Status;
                existingCase.Priority = request.Priority;
                existingCase.Type = request.Type;
                existingCase.LastEditedById = userId;
                existingCase.LastModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Case ID {Id} successfully updated by User ID: {UserId}", id, userId);

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update failed for PATCH request on case ID: {Id}. Request: {@Request}. User ID: {UserId}", id, request, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the case.");
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Failed to map PATCHCase DTO to Case for PATCH request on case ID: {Id}. User ID: {UserId}", id, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while patching case ID: {Id}. User ID: {UserId}", id, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCase([FromRoute] string id)
        {
            _logger.LogInformation("Delete request received for case {Id}", id);

            try
            {
                var caseToDelete = await _context.Cases.FindAsync(id);

                if (caseToDelete is null)
                {
                    return NotFound("Case not found");
                }

                _context.Remove(caseToDelete);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Delete request for case {Id} successful", id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete a case of Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}