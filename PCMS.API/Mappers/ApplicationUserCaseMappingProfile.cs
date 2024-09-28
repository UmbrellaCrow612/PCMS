using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class ApplicationUserCaseMappingProfile : Profile
    {
        public ApplicationUserCaseMappingProfile()
        {
            CreateMap<ApplicationUserCase, GETApplicationUserCase>();
        }
    }
}