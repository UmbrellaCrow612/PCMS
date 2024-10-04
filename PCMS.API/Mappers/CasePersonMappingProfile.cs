using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.Models;

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