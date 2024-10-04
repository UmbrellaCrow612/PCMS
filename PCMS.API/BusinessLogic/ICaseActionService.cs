using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.PATCH;

namespace PCMS.API.BusinessLogic
{
    /// <summary>
    /// Interface for implementing the business logic for case action service.
    /// </summary>
    public interface ICaseActionService
    {
        /// <summary>
        /// Create a case action for a case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="userId">The ID of the user creating the case action.</param>
        /// <param name="request">The data used to create the case action.</param>
        /// <returns>DTO <see cref="CaseActionDto"/> representing the newly created case action or null if the case dose not exist.</returns>
        Task<CaseActionDto?> CreateCaseActionAsync(string caseId, string userId, CreateCaseActionDto request);

        /// <summary>
        /// Get a case action by its ID.
        /// </summary>
        /// <param name="caseActionId">The ID of the case action.</param>
        /// <returns>DTO <see cref="CaseActionDto"/> or null if not found.</returns>
        Task<CaseActionDto?> GetCaseActionByIdAsync(string caseActionId, string caseId);

        /// <summary>
        /// Get all case actions for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>List of <see cref="CaseActionDto"/> associated with the case.</returns>
        Task<List<CaseActionDto>> GetCaseActionsForCaseIdAsync(string caseId);

        /// <summary>
        /// Update a case action with new data.
        /// </summary>
        /// <param name="caseActionId">The ID of the case action.</param>
        /// <param name="userId">The ID of the user updating the case action.</param>
        /// <param name="request">The new data to update the case action.</param>
        /// <returns>DTO <see cref="CaseActionDto"/> or null if not found.</returns>
        Task<CaseActionDto?> UpdateCaseActionByIdAsync(string caseActionId, string caseId, string userId, UpdateCaseActionDto request);

        /// <summary>
        /// Delete a case action.
        /// </summary>
        /// <param name="caseActionId">The ID of the case action.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>True if deleted successfully, false if the case action does not exist.</returns>
        Task<bool> DeleteCaseActionByIdAsync(string caseActionId, string caseId);
    }
}
