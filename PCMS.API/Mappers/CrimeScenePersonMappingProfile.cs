using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CrimeScenePersonMappingProfile : Profile
    {
        public CrimeScenePersonMappingProfile()
        {
            CreateMap<CrimeScenePerson, GETCrimeScenePerson>();
        }
    }
}