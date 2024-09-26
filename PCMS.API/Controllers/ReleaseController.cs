using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;
using SQLitePCL;
using Microsoft.AspNetCore.Http;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Handle release related actions for a booking
    /// </summary>
    /// <remarks>
    /// Note a release is a one to on relation to a booking, hence the singular noun endpoint, as a booking only ever has 
    /// one release associated with it, so we dont need its ID as we can perform actions needed with the ID of the person and booking. 
    /// </remarks>
    [ApiController]
    [Route("persons/{id}/bookings/{bookingId}/release")]
    public class ReleaseController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GETRelease>> CreateRelease(string id, string bookingId, [FromBody] POSTRelease request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person dose not exist.");
            }

            var booking = await _context.Bookings.Where(x => x.Id == bookingId && x.PersonId == id).Include(x => x.Release).FirstOrDefaultAsync();
            if (booking is null)
            {
                return NotFound("Booking dose not exist or is linked to this person.");
            }

            if (booking.Release is not null)
            {
                return BadRequest("There is already a release for this booking.");
            }

            var release = _mapper.Map<Release>(request);
            release.UserId = userId;
            release.PersonId = id;
            release.BookingId = bookingId;

            await _context.Releases.AddAsync(release);
            await _context.SaveChangesAsync();

            var returnRelease = _mapper.Map<GETRelease>(release);
            return CreatedAtAction(nameof(CreateRelease), returnRelease);
        }

        [HttpGet]
        public async Task<ActionResult> GetRelease(string id, string bookingId)
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> PatchRelease(string id, string bookingId)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRelease(string id, string bookingId)
        {
            return Ok();
        }
    }
}
