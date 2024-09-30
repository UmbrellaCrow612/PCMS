using PCMS.API.Dtos.GET;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models.Enums;

namespace PCMS.API.BusinessLogic
{
    public interface ICaseService
    {
        /// <summary>
        /// Create a case
        /// </summary>
        /// <param name="request">The data sent to create the case.</param>
        /// <param name="userId">The ID of the user who made the request to make the case.</param>
        /// <returns>DTO <see cref="GETCase"/> of the created case.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the case creation fails.</exception>
        Task<GETCase> CreateCaseAsync(POSTCase request, string userId);

        /// <summary>
        /// Get a <see cref="Case"/> by its ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>DTO <see cref="GETCase"/> or null if it could not find it.</returns>
        Task<GETCase?> GetCaseByIdAsync(string id);

        /// <summary>
        /// Update a case by its ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <param name="request">The new Case data sent across</param>
        /// <param name="userId">The ID of the user making the update.</param>
        /// <returns>Updated <see cref="GETCase"/> if successful, null if the case was not found.</returns>
        Task<GETCase?> UpdateCaseByIdAsync(string id, string userId, PATCHCase request);

        /// <summary>
        /// Deletes a case by its ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>True if the case was deleted, false if it was not found.</returns>
        Task<bool> DeleteCaseByIdAsync(string id);

        /// <summary>
        /// Gets Persons linked to this case
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>List of <see cref="GETCasePerson"/> or empty array</returns>
        Task<List<GETCasePerson>> GetCasePersonsAsync(string id);

        /// <summary>
        /// Get all users linked to this case
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>List of <see cref="GETApplicationUser"/> or empty</returns>
        Task<List<GETApplicationUser>> GetCaseUsersAsync(string id);

        /// <summary>
        /// Gets all the edits made on a case
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>List of <see cref="GETCaseEdit"/> or empty</returns>
        Task<List<GETCaseEdit>> GetCaseEditsByIdAsync(string id);

        /// <summary>
        /// Get all the tags linked to a case.
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>List of <see cref="GETTag"/> or empty.</returns>
        Task<List<GETTag>> GetCaseTagsAsync(string id);
    }
}
