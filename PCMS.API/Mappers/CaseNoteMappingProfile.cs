using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CaseNoteMappingProfile : Profile
    {
        public CaseNoteMappingProfile()
        {
            CreateMap<POSTCaseNote, CaseNote>();
            CreateMap<CaseNote, GETCaseNote>();
            CreateMap<PATCHCaseNote, CaseNote>();
        }
    }
}