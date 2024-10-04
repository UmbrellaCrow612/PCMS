using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.DTOS.Read;

namespace PCMS.API.Mappers
{
    public class ApplicationUserCaseMappingProfile : Profile
    {
        public ApplicationUserCaseMappingProfile()
        {
            CreateMap<ApplicationUserCase, ApplicationUserCaseDto>();
        }
    }
}