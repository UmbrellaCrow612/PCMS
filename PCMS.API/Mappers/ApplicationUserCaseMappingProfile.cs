using AutoMapper;
using PCMS.API.DTOS.Read;
using PCMS.API.Models;

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