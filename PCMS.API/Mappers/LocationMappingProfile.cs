using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.DTOS.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.Mappers
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<CreateLocationDto, Location>();
            CreateMap<Location, LocationDto>();
            CreateMap<UpdateLocationDto, Location>();
        }
    }
}