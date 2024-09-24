using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<POSTLocation, Location>();
            CreateMap<Location, GETLocation>();
            CreateMap<PATCHLocation, Location>();
        }
    }
}
