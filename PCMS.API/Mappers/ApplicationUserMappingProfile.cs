using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<CreateRegisterRequestDto, ApplicationUser>();
        }
    }
}