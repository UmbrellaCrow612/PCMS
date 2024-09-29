using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.POST;

namespace PCMS.API.BusinessLogic
{
    public interface ICaseService
    {
        Task<GETCase> CreateCaseAsync(POSTCase request, string userId);
    }
}
