using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;

namespace PCMS.API.BusinessLogic
{
    /// <summary>
    /// Interface for implementing the business logic for Report operations.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Creates a report for a case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="userId">The ID of the user creating the report.</param>
        /// <param name="request">The data to create the report.</param>
        /// <returns>The newly created report or null if the case dose not exist.</returns>
        Task<GETReport?> CreateReportAsync(string caseId, string userId, POSTReport request);

        /// <summary>
        /// Retrieves a specific report.
        /// </summary>
        /// <param name="reportId">The ID of the report.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>The requested report, or null if not found.</returns>
        Task<GETReport?> GetReportByIdAsync(string reportId, string caseId);

        /// <summary>
        /// Get all reports for a case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>List of reports or null if the case dose not exist.</returns>
        Task<List<GETReport>?> GetReportsForCaseIdAsync(string caseId);

        /// <summary>
        /// Updates a report.
        /// </summary>
        /// <param name="reportId">The ID of the report to update.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="request">The updated report data.</param>
        /// <returns>The updated report, or null if not found.</returns>
        Task<GETReport?> UpdateReportByIdAsync(string reportId, string caseId, string userId, PATCHReport request);

        /// <summary>
        /// Deletes a report.
        /// </summary>
        /// <param name="reportId">The ID of the report to delete.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>True if the report was deleted, false if it does not exist.</returns>
        Task<bool> DeleteReportByIdAsync(string reportId, string caseId, string userId);
    }
}