using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

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