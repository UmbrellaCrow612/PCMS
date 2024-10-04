using AutoMapper;
using PCMS.API.Dtos.Read;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CrimeScenePersonMappingProfile : Profile
    {
        public CrimeScenePersonMappingProfile()
        {
            CreateMap<CrimeScenePerson, CrimeScenePersonDto>();
        }
    }
}