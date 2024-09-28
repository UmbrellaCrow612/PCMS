using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Handle booking related actions - for the base booking CRUD
    /// </summary>
    [ApiController]
    [Route("persons/{id}/bookings")]
    [Authorize]
    public class BookingController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GETBooking>> CreateBooking(string id, [FromBody] POSTBooking request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found");
            }

            var locationExists = await _context.Locations.AnyAsync(l => l.Id == request.LocationId);
            if (!locationExists)
            {
                return NotFound("Location not found");
            }

            var booking = _mapper.Map<Booking>(request);
            booking.UserId = userId;
            booking.PersonId = id;

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            var _booking = await _context.Bookings.Where(b => b.Id == booking.Id)
                .Include(b => b.User)
                .Include(b => b.Person)
                .Include(b => b.Release)
                .Include(b => b.Charges)
                .Include(b => b.Location)
                .FirstOrDefaultAsync() ?? throw new ApplicationException("Failed to get created booking");

            var returnBooking = _mapper.Map<GETBooking>(_booking);

            return CreatedAtAction(nameof(CreateBooking), new { id = returnBooking.Id }, returnBooking);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETBooking>>> GetBookings(string id)
        {
            var personExists = await _context.Persons.AnyAsync(p => p.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var bookings = await _context.Bookings.Where(b => b.PersonId == id)
                .Include(b => b.User)
                .Include(b => b.Person)
                .Include(b => b.Release)
                .Include(b => b.Charges)
                .Include(b => b.Location)
                .ToListAsync();

            var returnBookings = _mapper.Map<List<GETBooking>>(bookings);
            return Ok(returnBookings);
        }

        [HttpPatch("{bookingId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateBooking(string id, string bookingId, [FromBody] PATCHBooking request)
        {
            var personExists = await _context.Persons.AnyAsync(p => p.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var booking = await _context.Bookings.Where(b => b.Id == bookingId && b.PersonId == id).FirstOrDefaultAsync();
            if (booking is null)
            {
                return NotFound("Booking not found or is linked to this person.");
            }

            _mapper.Map(request, booking);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{bookingId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteBooking(string id, string bookingId)
        {
            var personExists = await _context.Persons.AnyAsync(p => p.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var booking = await _context.Bookings.Where(b => b.Id == bookingId).FirstOrDefaultAsync();
            if (booking is null)
            {
                return NotFound("Booking not found.");
            }

            _context.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}