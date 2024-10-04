using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.Mappers
{
    public class ReleaseMappingProfile : Profile
    {
        public ReleaseMappingProfile()
        {
            CreateMap<CreateReleaseDto, Release>();
            CreateMap<Release, ReleaseDto>();
            CreateMap<UpdateReleaseDto, Release>();
        }
    }
}