using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling report-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReportController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">Automapper.</param>
    /// <param name="userManager">Identity helper service.</param>
    [ApiController]
    [Route("/cases/{caseId}/reports")]
    [Produces("application/json")]
    [Authorize]
    public class ReportController(ILogger<CaseController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<CaseController> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a report.
        /// </summary>
        /// <param name="request">The DTO containing POST report information.</param>
        /// <returns>The created report details.</returns>
        /// <response code="201">Returns the newly created report.</response>
        /// <response code="400">Returns when the request is invalid.</response>
        /// <response code="401">Returns when the request is unauthorized.</response>
        /// <response code="500">Returns when there's an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GETReport), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GETReport>> CreateReport([FromRoute][Required] string caseId, [FromBody] POSTReport request)
        {
            _logger.LogInformation("POST report request received for case ID: {CaseId}", caseId);

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

                var report = _mapper.Map<Report>(request);
                report.CreatedById = userId;
                report.LastEditedById = userId;
                report.CaseId = caseId;

                await _context.Reports.AddAsync(report);
                await _context.SaveChangesAsync();

                var returnReport = _mapper.Map<GETReport>(report);

                _logger.LogInformation("Created a new report with ID: {id} for case ID: {CaseId}", report.Id, caseId);

                return CreatedAtAction(nameof(CreateReport), new { caseId, id = returnReport.Id }, returnReport);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a new report. CaseId: {CaseId}, Request: {@Request}", caseId, request);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
