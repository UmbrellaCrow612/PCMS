using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.BusinessLogic.Interfaces
{
    /// <summary>
    /// Contract for the implementation of the Tag service.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Creates a new tag.
        /// </summary>
        /// <param name="userId">The ID of the user creating the tag.</param>
        /// <param name="request">The tag data.</param>
        /// <returns>The newly created tag.</returns>
        Task<TagDto> CreateTagAsync(string userId, CreateTagDto request);

        /// <summary>
        /// Retrieves a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag.</param>
        /// <returns>The tag, or null if not found.</returns>
        Task<TagDto?> GetTagByIdAsync(string tagId);

        /// <summary>
        /// Updates a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag.</param>
        /// <param name="userId">The ID of the user updating the tag.</param>
        /// <param name="request">The updated tag data.</param>
        /// <returns>True if it did, or false if not found.</returns>
        Task<bool> UpdateTagByIdAsync(string tagId, string userId, UpdateTagDto request);

        /// <summary>
        /// Deletes a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag.</param>
        /// <returns>True if the tag was deleted, or false if it does not exist or is linked to a case.</returns>
        Task<bool> DeleteTagByIdAsync(string tagId);

        /// <summary>
        /// Retrieves all tags linked to a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <returns>A list of tags, or null if the case does not exist.</returns>
        Task<List<TagDto>?> GetTagsForCaseIdAsync(string caseId);

        /// <summary>
        /// Links a tag to a case.
        /// </summary>
        /// <param name="tagId">The ID of the tag to link.</param>
        /// <param name="caseId">The ID of the case to link the tag to.</param>
        /// <returns>True if the tag was successfully linked, or false if the tag or case does not exist, or if the link already exists.</returns>
        Task<bool> LinkTagToCase(string tagId, string caseId);

        /// <summary>
        /// Un-link a tag from a case
        /// </summary>
        /// <param name="tagId">The ID of the tag to un-link</param>
        /// <param name="caseId">The ID of the case to un-link from.</param>
        /// <returns>True if could, false if either; the case or tag is null or they are not linked already.</returns>
        Task<bool> UnLinkTagFromCase(string tagId, string caseId);
    }
}
