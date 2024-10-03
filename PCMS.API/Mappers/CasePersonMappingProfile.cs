using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CasePersonMappingProfile : Profile
    {
        public CasePersonMappingProfile()
        {
            CreateMap<POSTCasePerson, CasePerson>();
            CreateMap<CasePerson, CasePersonDto>();
        }
    }
}