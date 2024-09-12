using AutoMapper;
using PCMS.API.DTOS;
using PCMS.API.Models;

namespace PCMS.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<POSTCase, Case>()
                      .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                      .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                      .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<Case, GETCase>()
                .ReverseMap(); 
        }
    }
}
