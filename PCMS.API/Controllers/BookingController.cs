using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Handle booking related actions - for the base booking CRUD
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
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
        public async Task<ActionResult> CreateBooking(string id, [FromBody] POSTBooking request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var locationExists = await _context.Locations.Where(l => l.Id == request.LocationId).FirstOrDefaultAsync();
            if (locationExists is null)
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
                .Include(b => b.Location)
                .FirstOrDefaultAsync() ?? throw new ApplicationException("Failed to get created booking");

            var returnBooking = _mapper.Map<GETBooking>(_booking);

            return CreatedAtAction(nameof(CreateBooking), new { id = returnBooking.Id }, returnBooking);
        }
    }
}
