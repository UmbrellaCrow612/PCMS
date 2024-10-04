using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.BusinessLogic;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.PATCH;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("/cases/{caseId}/reports")]
    [Authorize]
    public class ReportController(IReportService reportService) : ControllerBase
    {
        private readonly IReportService _reportService = reportService;

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<ActionResult<ReportDto>> CreateReport(string caseId, [FromBody] CreateReportDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var report = await _reportService.CreateReportAsync(caseId, userId, request);

            if (report is null)
            {
                return NotFound("case not found.");
            }

            return report;
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ReportDto>>> GetReports(string caseId)
        {
           var reports = await _reportService.GetReportsForCaseIdAsync(caseId);

            if (reports is null)
            {
                return NotFound("Case not found.");
            }

            return Ok(reports);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReportDto>> GetReport(string caseId, string id)
        {
            var report = await _reportService.GetReportByIdAsync(id, caseId);

            if (report is null)
            {
                return NotFound("Report not found.");
            }

            return Ok(report);
        }

        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PatchReport(string caseId, string id, [FromBody] UpdateReportDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var updatedReport = await _reportService.UpdateReportByIdAsync(id, caseId, userId, request);

            if (updatedReport is null)
            {
                return NotFound("Report not found.");
            }

            return Ok(updatedReport);
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteReport(string caseId, string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var isDeleted = await _reportService.DeleteReportByIdAsync(id, caseId, userId);
            if (!isDeleted)
            {
                return NotFound("Report not found.");
            }

            return NoContent();
        }
    }
}