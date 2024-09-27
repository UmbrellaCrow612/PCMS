using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CrimeSceneMappingProfile : Profile
    {
        public CrimeSceneMappingProfile()
        {
            CreateMap<POSTCrimeScene, CrimeScene>();
            CreateMap<CrimeScene, GETCrimeScene>();
            CreateMap<PATCHCrimeScene, CrimeScene>();
        }
    }
}
