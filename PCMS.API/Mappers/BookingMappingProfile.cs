using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<Booking, BookingDto>();
            CreateMap<UpdateBookingDto, Booking>();
        }
    }
}