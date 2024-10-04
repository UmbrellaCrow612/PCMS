using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.DTOS.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.Mappers
{
    public class EvidenceMappingProfile : Profile
    {
        public EvidenceMappingProfile()
        {
            CreateMap<CreateEvidenceDto, Evidence>();
            CreateMap<Evidence, EvidenceDto>();
            CreateMap<UpdateEvidenceDto, Evidence>();
        }
    }
}