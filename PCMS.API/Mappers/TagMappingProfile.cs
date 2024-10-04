using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<CreateTagDto, Tag>();
            CreateMap<Tag, TagDto>();
            CreateMap<UpdateTagDto, Tag>();
        }
    }
}