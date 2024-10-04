using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Read;

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