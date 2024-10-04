using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;

namespace PCMS.API.Mappers
{
    public class CasePersonMappingProfile : Profile
    {
        public CasePersonMappingProfile()
        {
            CreateMap<CreateCasePersonDto, CasePerson>();
            CreateMap<CasePerson, CasePersonDto>();
        }
    }
}