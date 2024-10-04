using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            CreateMap<CreateCaseDto, Case>();
            CreateMap<Case, CaseDto>();
            CreateMap<UpdateCaseDto, Case>();
        }
    }
}