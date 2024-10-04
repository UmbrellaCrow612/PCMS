using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.PATCH;

namespace PCMS.API.BusinessLogic
{
    /// <summary>
    /// Implementation standard for the case note service.
    /// </summary>
    public interface ICaseNoteService
    {
        /// <summary>
        /// Creates a case note.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="request">The data for creating the case note.</param>
        /// <returns>The created case note or null if the case was not found.</returns>
        Task<CaseNoteDto?> CreateCaseNoteAsync(string caseId, string userId, CreateCaseNoteDto request);

        /// <summary>
        /// Retrieves all case notes for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>A list of case notes or null if the case was not found.</returns>
        Task<List<CaseNoteDto>?> GetCaseNotesForCaseIdAsync(string caseId);

        /// <summary>
        /// Retrieves a specific case note by its ID.
        /// </summary>
        /// <param name="caseNoteId">The ID of the case note.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>The case note or null if it was not found.</returns>
        Task<CaseNoteDto?> GetCaseNoteByIdAsync(string caseNoteId, string caseId);

        /// <summary>
        /// Updates an existing case note.
        /// </summary>
        /// <param name="caseNoteId">The ID of the case note.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="request">The data for updating the case note.</param>
        /// <returns>The updated case note or null if it was not found.</returns>
        Task<CaseNoteDto?> UpdateCaseNoteByIdAsync(string caseNoteId, string caseId, string userId, UpdateCaseNoteDto request);

        /// <summary>
        /// Deletes a specific case note.
        /// </summary>
        /// <param name="caseNoteId">The ID of the case note.</param>
        /// <param name="caseId">The ID of the case it is linked to.</param>
        /// <param name="userId">The ID of the user performing the action.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        Task<bool> DeleteCaseNoteByIdAsync(string caseNoteId, string caseId, string userId);
    }
}
