using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.PATCH;

namespace PCMS.API.BusinessLogic
{
    public interface ICaseService
    {
        /// <summary>
        /// Create a case
        /// </summary>
        /// <param name="request">The data sent to create the case.</param>
        /// <param name="userId">The ID of the user who made the request to make the case.</param>
        /// <returns>DTO <see cref="CaseDto"/> of the created case.</returns>
        Task<CaseDto> CreateCaseAsync(CreateCaseDto request, string userId);

        /// <summary>
        /// Get a <see cref="Case"/> by its ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>DTO <see cref="CaseDto"/> or null if it could not find it.</returns>
        Task<CaseDto?> GetCaseByIdAsync(string id);

        /// <summary>
        /// Update a case by its ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <param name="request">The new Case data sent across</param>
        /// <param name="userId">The ID of the user making the update.</param>
        /// <returns>True if it was able to update the case else false if it could not find it.</returns>
        Task<bool> UpdateCaseByIdAsync(string id, string userId, UpdateCaseDto request);

        /// <summary>
        /// Deletes a case by its ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>True if the case was soft deleted, false if it was not found.</returns>
        Task<bool> DeleteCaseByIdAsync(string id, string userId);

        /// <summary>
        /// Gets Persons linked to this case
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>List of <see cref="CasePersonDto"/> or empty array</returns>
        Task<List<CasePersonDto>> GetLinkedPersonsForCaseIdAsync(string id);

        /// <summary>
        /// Get all users linked to this case
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>List of <see cref="ApplicationUserDto"/> or empty</returns>
        Task<List<ApplicationUserDto>> GetLinkedUsersForCaseIdAsync(string id);

        /// <summary>
        /// Gets all the edits made on a case
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>List of <see cref="CaseEditDto"/> or empty</returns>
        Task<List<CaseEditDto>> GetCaseEditsForCaseIdAsync(string id);

        /// <summary>
        /// Get all the tags linked to a case.
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>List of <see cref="TagDto"/> or empty.</returns>
        Task<List<TagDto>> GetLinkedTagsForCaseIdAsync(string id);

        /// <summary>
        /// Get cases based on search params.
        /// </summary>
        /// <param name="request">The data.</param>
        /// <returns>List of cases</returns>
        Task<List<CaseDto>> SearchForCases(CreateCaseSearchDto request);
    }
}
