using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<POSTProperty, Property>();
            CreateMap<Property, GETProperty>();
            CreateMap<PATCHProperty, Property>();
        }
    }
}