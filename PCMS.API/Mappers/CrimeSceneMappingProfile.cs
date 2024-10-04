using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;
using PCMS.API.Models;

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