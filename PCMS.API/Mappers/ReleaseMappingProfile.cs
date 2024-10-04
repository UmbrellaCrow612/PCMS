using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Models;

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