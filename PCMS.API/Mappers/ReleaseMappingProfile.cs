using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class ReleaseMappingProfile : Profile
    {
        public ReleaseMappingProfile()
        {
            CreateMap<CreateReleaseDto, Release>();
            CreateMap<Release, ReleaseDto>();
            CreateMap<PATCHRelease, Release>();
        }
    }
}