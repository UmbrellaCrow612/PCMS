using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.PATCH;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CaseActionMappingProfile : Profile
    {
        public CaseActionMappingProfile()
        {
            CreateMap<CreateCaseActionDto, CaseAction>();
            CreateMap<CaseAction, CaseActionDto>();
            CreateMap<UpdateCaseActionDto, CaseAction>();
        }
    }
}