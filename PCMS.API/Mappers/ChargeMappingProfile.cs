using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.Mappers
{
    public class ChargeMappingProfile : Profile
    {
        public ChargeMappingProfile()
        {
            CreateMap<CreateChargeDto, Charge>();
            CreateMap<Charge, ChargeDto>();
            CreateMap<UpdateChargeDto, Charge>();
        }
    }
}