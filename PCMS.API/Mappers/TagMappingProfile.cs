using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<POSTTag, Tag>();
            CreateMap<Tag, TagDto>();
            CreateMap<PATCHTag, Tag>();
        }
    }
}