using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            CreateMap<POSTVehicle, Vehicle>();
            CreateMap<Vehicle, GETVehicle>();
            CreateMap<PATCHVehicle, Vehicle>();
        }
    }
}
