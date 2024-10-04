using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<CreatePropertyDto, Property>();
            CreateMap<Property, PropertyDto>();
            CreateMap<UpdatePropertyDto, Property>();
        }
    }
}