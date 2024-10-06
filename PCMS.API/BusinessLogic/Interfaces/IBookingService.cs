using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.BusinessLogic.Interfaces
{
    /// <summary>
    /// Business logic contract for the service implamentation.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Create a booking for a person.
        /// </summary>
        /// <param name="personId">The ID of the person for who the booking is for.</param>
        /// <param name="userId">The ID of the user making the request.</param>
        /// <param name="request">The data.</param>
        /// <returns>The booking or null if the person dose not exist.</returns>
        Task<BookingDto?> CreateBookingAsync(string personId, string userId, CreateBookingDto request);

        /// <summary>
        /// Get a booking by ID.
        /// </summary>
        /// <param name="bookingId">The ID of the booking.</param>
        /// <returns>The Booking or null if it dose not exist.</returns>
        Task<BookingDto?> GetBookingByIdAsync(string bookingId);

        /// <summary>
        /// Get all bookings for a person.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>List of bookings or null if the person dose not exist.</returns>
        Task<List<BookingDto>?> GetBookingsForPersonAsync(string personId);

        /// <summary>
        /// Update a booking.
        /// </summary>
        /// <param name="bookingId">The ID of the booking.</param>
        /// <param name="userId">The ID of the user making the request.</param>
        /// <param name="request">The new data.</param>
        /// <returns>True if it could false if the booking was not found or person.</returns>
        Task<bool> UpdateBookingByIdAsync(string personId, string bookingId, string userId, UpdateBookingDto request);

        /// <summary>
        /// Update a booking.
        /// </summary>
        /// <param name="personId">The ID of the person the booking is for.</param>
        /// <param name="bookingId">The ID of the booking to update.</param>
        /// <returns>True if it could delete it or false if booking or person was not found.</returns>
        Task<bool> DeleteBookingByIdAsync(string personId, string bookingId);

        /// <summary>
        /// Search for bookings based on query params.
        /// </summary>
        /// <param name="request">The params</param>
        /// <returns>List of bookings.</returns>
        Task<List<BookingDto>> SearchBookings(CreateSearchBookingQueryDto request);

    }
}
