using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
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
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Get all reports.
        /// </summary>
        /// <returns>The list of reports for a case.</returns>
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETReport>>> GetReports([FromRoute][Required] string caseId)
        {
            _logger.LogInformation("GET reports request received for case ID: {CaseId}", caseId);

            try
            {
                if (string.IsNullOrEmpty(caseId))
                {
                    return BadRequest("Case ID cannot be null or empty");
                }

                var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);

                if (!caseExists)
                {
                    return NotFound("Case not found");
                }

                var existingReports = await _context.Reports.Where(r => r.CaseId == caseId).ToListAsync();

                if (existingReports.Count is 0)
                {
                    return Ok(new List<GETReport>());
                }

                var returnReports = _mapper.Map<List<GETReport>>(existingReports);

                return Ok(returnReports);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get reports. CaseId: {CaseId}", caseId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Get a report.
        /// </summary>
        /// <param name="caseId">The case ID</param>
        /// <param name="id">The ID of the report</param>
        /// <returns>The report for a case.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETReport>> GetReport([FromRoute][Required] string caseId, [FromRoute][Required] string id)
        {
            _logger.LogInformation("GET report request received for case ID: {caseId} report ID: {id}", caseId, id);

            try
            {
                if (string.IsNullOrEmpty(caseId) | string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID or Report ID is null or empty");
                }

                var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);

                if (!caseExists)
                {
                    return NotFound("Case not found");
                }

                var reportExists = await _context.Reports.AnyAsync(r => r.Id == id);

                if (!reportExists)
                {
                    return NotFound("Report not found");
                }

                var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id && r.CaseId == caseId);

                if (report is null)
                {
                    return NotFound("Report not found");
                }

                var returnReport = _mapper.Map<GETReport>(report);

                return Ok(returnReport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get report. CaseId: {caseId} report ID: {id}", caseId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Patch a report.
        /// </summary>
        /// <param name="caseId">The case ID</param>
        /// <param name="id">The ID of the report</param>
        /// <param name="request">The DTO for post report information</param>
        /// <returns>No content</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchReport([FromRoute][Required] string caseId, [FromRoute][Required] string id, [FromBody] PATCHReport request)
        {
            _logger.LogInformation("PATCH report request received for case ID: {caseId} report ID: {id} request: {request}", caseId, id, request);

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
                if (string.IsNullOrEmpty(caseId) | string.IsNullOrEmpty(id))
                {
                    return BadRequest("Case ID or report ID is null or empty");
                }

                var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);

                if (!caseExists)
                {
                    return NotFound("Case not found");
                }

                var reportExists = await _context.Reports.AnyAsync(r => r.Id == id);

                if (!reportExists)
                {
                    return NotFound("Report not found");
                }

                var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id && r.CaseId == caseId);

                if (report is null)
                {
                    return NotFound("Report not found or is linked to this case");
                }

                report.Title = request.Title;
                report.Details = request.Details;
                report.LastEditedById = userId;
                report.LastModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to patch report. CaseId: {caseId} report ID: {id} request: {request}", caseId, id, request);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
