using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<UpdateVehicleDto, Vehicle>();
        }
    }
}
