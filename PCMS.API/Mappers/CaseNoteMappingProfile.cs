using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CaseNoteMappingProfile : Profile
    {
        public CaseNoteMappingProfile()
        {
            CreateMap<CreateCaseNoteDto, CaseNote>();
            CreateMap<CaseNote, CaseNoteDto>();
            CreateMap<UpdateCaseNoteDto, CaseNote>();
        }
    }
}