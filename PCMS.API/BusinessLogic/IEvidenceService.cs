using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;

namespace PCMS.API.BusinessLogic
{
    /// <summary>
    /// Interface to implement the bussiness logic for Evidence
    /// </summary>
    public interface IEvidenceService
    {
        /// <summary>
        /// Create Evidence 
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="request">The data.</param>
        /// <returns>Newly created Evidence or null if the case was not found.</returns>
        Task<EvidenceDto?> CreateEvidenceAsync(string caseId, string userId, CreateEvidenceDto request);

        /// <summary>
        /// Get Evidence item.
        /// </summary>
        /// <param name="evidenceId">The ID of the Evidence.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>The Evidence item or null if the case dose not exist.</returns>
        Task<EvidenceDto?> GetEvidenceByIdAsync(string evidenceId, string caseId);

        /// <summary>
        /// The Evidence for a case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>List of Evidence or null if the case dose not exist.</returns>
        Task<List<EvidenceDto>?> GetEvidenceForCaseIdAsync(string caseId);

        /// <summary>
        /// Update a Evidence
        /// </summary>
        /// <param name="evidenceId">The ID of the Evidence.</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="request">The data.</param>
        /// <returns>The updated Evidence or null if it could not find it..</returns>
        Task<EvidenceDto?> UpdatetEvidenceByIdAsync(string evidenceId, string caseId, string userId,PATCHEvidence request);

        /// <summary>
        /// Delete an Evidence item.
        /// </summary>
        /// <param name="evidenceId">The ID of the Evidence</param>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>True if it was deleted or false if it could not find it.</returns>
        Task<bool> DeleteEvidenceByIdAsync(string evidenceId, string caseId, string userId);
    }
}
