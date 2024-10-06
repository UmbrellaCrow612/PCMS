using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        public Task<BookingDto?> CreateBookingAsync(string personId, string userId, CreateBookingDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookingByIdAsync(string personId, string bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<BookingDto?> GetBookingByIdAsync(string bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookingDto>?> GetBookingsForPersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookingDto>> SearchBookings(CreateSearchBookingQueryDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookingByIdAsync(string personId, string bookingId, string userId, UpdateBookingDto request)
        {
            throw new NotImplementedException();
        }
    }
}
