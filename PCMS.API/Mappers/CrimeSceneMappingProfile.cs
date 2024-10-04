using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.Mappers
{
    public class CrimeSceneMappingProfile : Profile
    {
        public CrimeSceneMappingProfile()
        {
            CreateMap<CreateCrimeSceneDto, CrimeScene>();
            CreateMap<CrimeScene, CrimeSceneDto>();
            CreateMap<UpdateCrimeSceneDto, CrimeScene>();
        }
    }
}