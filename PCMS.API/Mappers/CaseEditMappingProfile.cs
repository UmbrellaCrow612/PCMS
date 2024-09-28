using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class CaseEditMappingProfile : Profile
    {
        public CaseEditMappingProfile()
        {
            CreateMap<Case, CaseEdit>()
                .ForMember(dest => dest.PreviousTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PreviousDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PreviousStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PreviousPriority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.PreviousComplexity, opt => opt.MapFrom(src => src.Complexity))
                .ForMember(dest => dest.PreviousType, opt => opt.MapFrom(src => src.Type));

            CreateMap<CaseEdit, GETCaseEdit>();
        }
    }
}