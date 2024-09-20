using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

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
    [Authorize]
    public class ReportController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {

        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a report.
        /// </summary>
        /// <param name="request">The DTO containing POST report information.</param>
        /// <returns>The created report details.</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<ActionResult<GETReport>> CreateReport(string caseId, [FromBody] POSTReport request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

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

            return CreatedAtAction(nameof(CreateReport), new { caseId, id = returnReport.Id }, returnReport);
        }

        /// <summary>
        /// Get all reports.
        /// </summary>
        /// <returns>The list of reports for a case.</returns>
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETReport>>> GetReports(string caseId)
        {
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

        /// <summary>
        /// Get a report.
        /// </summary>
        /// <param name="caseId">The case ID</param>
        /// <param name="id">The ID of the report</param>
        /// <returns>The report for a case.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETReport>> GetReport(string caseId, string id)
        {
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
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<ActionResult> PatchReport(string caseId, string id, [FromBody] PATCHReport request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

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

        /// <summary>
        /// Delete a report.
        /// </summary>
        /// <param name="caseId">The case ID</param>
        /// <param name="id">The ID of the report</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteReport(string caseId, string id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id && r.CaseId == caseId);

            if (report is null)
            {
                return NotFound("Report dose not exist or is linked to this case");
            }

            _context.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}