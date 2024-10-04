using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

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