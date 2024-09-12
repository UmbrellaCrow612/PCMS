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
    /// Controller for handling case-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CaseController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    [ApiController]
    [Route("cases")]
    [Produces("application/json")]
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
        /// <response code="201">Returns the newly created case.</response>
        /// <response code="400">Returns when the request is invalid.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GETCase), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETCase>> CreateCase([FromBody] POSTCase request)
        {
            try
            {
                _logger.LogInformation("POST case request received");

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }

                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                {
                    return Unauthorized("Unauthorized");
                }

                var newCase = _mapper.Map<Case>(request);
                newCase.CreatedById = userId;
                newCase.LastEditedById = userId;

                await _context.Cases.AddAsync(newCase);
                await _context.SaveChangesAsync();

                var createdCase = await _context.Cases
                    .Include(c => c.CaseActions)
                    .Include(c => c.AssignedUsers)
                    .Include(c => c.Reports)
                    .FirstOrDefaultAsync(c => c.Id == newCase.Id) ?? throw new Exception("Failed to retrieve the created case");

                // Use AutoMapper to map from Case model to GETCase DTO
                var getCaseResult = _mapper.Map<GETCase>(createdCase);

                _logger.LogInformation("Created a new case with ID: {CaseId}", getCaseResult.Id);

                return CreatedAtAction(nameof(GetCase), new { id = getCaseResult.Id }, getCaseResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a new case. Request: {@Request}", request);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>The requested case.</returns>
        /// <response code="200">Returns the requested case.</response>
        /// <response code="400">Returns when the ID is invalid.</response>
        /// <response code="404">Returns when the case is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GETCase), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETCase>> GetCase([FromRoute][Required] string id)
        {
            try
            {
                _logger.LogInformation("Get case request received for id: {Id}", id);

                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID cannot be null or empty.");
                }

                var caseResult = await _context.Cases
                    .Select(c => new GETCase
                    {
                        Id = c.Id,
                        CaseNumber = c.CaseNumber,
                        Title = c.Title,
                        Description = c.Description,
                        Status = c.Status,
                        DateOpened = c.DateOpened,
                        DateClosed = c.DateClosed,
                        LastModifiedDate = c.LastModifiedDate,
                        Priority = c.Priority,
                        Type = c.Type,
                        CreatedById = c.CreatedById,
                        LastEditedById = c.LastEditedById,
                        CaseActions = c.CaseActions.Select(ca => new GETCaseAction
                        {
                            Id = ca.Id,
                            Name = ca.Name,
                            Description = ca.Description,
                            Type = ca.Type,
                            CreatedAt = ca.CreatedAt,
                            CreatedById = ca.CreatedById,
                            LastEditedById = ca.LastEditedById,
                            LastModifiedDate = ca.LastModifiedDate,
                        }).ToList(),
                        Reports = c.Reports,
                        AssignedUsers = c.AssignedUsers.Select(u => new GETApplicationUser
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Rank = u.Rank,
                            BadgeNumber = u.BadgeNumber,
                            DOB = u.DOB,
                            UserName = u.UserName!,
                            Email = u.Email!
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (caseResult is null)
                {
                    return NotFound($"Case with ID '{id}' was not found.");
                }

                _logger.LogInformation("Get case request for id: {Id} successful", id);

                return Ok(caseResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get a case for Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves all cases.
        /// </summary>
        /// <returns>A list of all cases.</returns>
        /// <response code="200">Returns the list of all cases.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GETCase>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GETCase>>> GetCases()
        {
            try
            {
                _logger.LogInformation("Get request received for all cases.");

                var cases = await _context.Cases
                    .Select(c => new GETCase
                    {
                        Id = c.Id,
                        CaseNumber = c.CaseNumber,
                        Title = c.Title,
                        Description = c.Description,
                        Status = c.Status,
                        DateOpened = c.DateOpened,
                        DateClosed = c.DateClosed,
                        LastModifiedDate = c.LastModifiedDate,
                        Priority = c.Priority,
                        Type = c.Type,
                        CreatedById = c.CreatedById,
                        LastEditedById = c.LastEditedById,
                        CaseActions = c.CaseActions.Select(ca => new GETCaseAction
                        {
                            Id = ca.Id,
                            Name = ca.Name,
                            Description = ca.Description,
                            Type = ca.Type,
                            CreatedAt = ca.CreatedAt,
                            CreatedById = ca.CreatedById,
                            LastEditedById = ca.LastEditedById,
                            LastModifiedDate = ca.LastModifiedDate,
                        }).ToList(),
                        Reports = c.Reports,
                        AssignedUsers = c.AssignedUsers.Select(u => new GETApplicationUser
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Rank = u.Rank,
                            BadgeNumber = u.BadgeNumber,
                            DOB = u.DOB,
                            UserName = u.UserName!,
                            Email = u.Email!
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(cases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get cases");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case to update.</param>
        /// <param name="request">The DTO containing the updated case information.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Returns when the case is successfully updated.</response>
        /// <response code="400">Returns when the request is invalid.</response>
        /// <response code="404">Returns when the case or user is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchCase([FromRoute][Required] string id, [FromBody] PATCHCase request)
        {
            try
            {
                _logger.LogInformation("Patch request received for case {Id}", id);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }

                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                {
                    return Unauthorized("Unauthorized");
                }

                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case id cannot be null or empty");
                }

                var existingCase = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);

                if (existingCase is null)
                {
                    return NotFound("Case not found");
                }

                existingCase.Title = request.Title;
                existingCase.Description = request.Description;
                existingCase.Status = request.Status;
                existingCase.Priority = request.Priority;
                existingCase.Type = request.Type;
                existingCase.LastEditedById = userId;
                existingCase.LastModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Patch request for case {Id} is successful", id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to patch a case of id: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Returns when the case is successfully deleted.</response>
        /// <response code="400">Returns when the ID is invalid.</response>
        /// <response code="404">Returns when the case is not found.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCase([FromRoute][Required] string id)
        {
            try
            {
                _logger.LogInformation("Delete request received for case {Id}", id);

                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case id cannot be null or empty");
                }

                var caseToDelete = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);

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