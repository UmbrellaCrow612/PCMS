using PCMS.API.Dtos.GET;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models.Enums;

namespace PCMS.API.BusinessLogic
{
    public interface ICaseService
    {
        Task<GETCase> CreateCaseAsync(POSTCase request, string userId);

        Task<GETCase> GetCaseByIdAsync(string id);

        Task<List<GETCase>> GetCasesBySearchAsync(
            CaseStatus? status,
            CaseComplexity? complexity,
            CasePriority? priority,
            DateTime? startDate,
            DateTime? endDate,
             string? type,
             string? createdById,
             string? departmentId,
              int page = 1,
               int pageSize = 10
            );

        Task UpdateCaseByIdAsync(string id, PATCHCase request);

        /// <summary>
        /// Deletes a case by it's ID
        /// </summary>
        /// <param name="id">The ID of the case</param>
        /// <returns>True or False depending on if it was able to delete the case by ID or the case did not exist.</returns>
        Task<bool> DeleteCaseByIdAsync(string id);

        Task<List<GETPerson>> GetCasePersonsAsync(string id);

        Task<List<GETApplicationUser>> GetCaseUsersAsync(string id);

        Task<List<GETCaseEdit>> GetCaseEditsByIdAsync(string id);

        Task<List<GETTag>> GetCaseTagsAsync(string id);
    }
}
