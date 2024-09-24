using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<POSTBooking, Booking>();
            CreateMap<Booking, GETBooking>();
            CreateMap<PATCHBooking, Booking>();
        }
    }
}
