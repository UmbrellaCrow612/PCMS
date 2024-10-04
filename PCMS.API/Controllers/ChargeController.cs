using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Handle charge related actions
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("persons/{id}/bookings/{bookingId}/charges")]
    public class ChargeController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;


        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateCharge(string id, string bookingId, [FromBody] CreateChargeDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var bookingExists = await _context.Bookings.AnyAsync(y => y.Id == bookingId);
            if (!bookingExists)
            {
                return NotFound("Booking not found.");
            }

            var charge = _mapper.Map<Charge>(request);
            charge.UserId = userId;
            charge.BookingId = bookingId;
            charge.PersonId = id;

            await _context.Charges.AddAsync(charge);
            await _context.SaveChangesAsync();

            var returnCharge = _mapper.Map<ChargeDto>(charge);

            return CreatedAtAction(nameof(CreateCharge), returnCharge);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCharges(string id, string bookingId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var bookingExists = await _context.Bookings.AnyAsync(y => y.Id == bookingId);
            if (!bookingExists)
            {
                return NotFound("Booking not found.");
            }

            var charges = await _context.Charges.Where(x => x.BookingId == bookingId && x.PersonId == id).ToListAsync();

            var returnCharges = _mapper.Map<List<ChargeDto>>(charges);
            return Ok(returnCharges);
        }

        [HttpGet("{chargeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCharge(string id, string bookingId, string chargeId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var bookingExists = await _context.Bookings.AnyAsync(y => y.Id == bookingId);
            if (!bookingExists)
            {
                return NotFound("Booking not found.");
            }


            var charge = await _context.Charges.Where(x => x.Id == chargeId).FirstOrDefaultAsync();
            if (charge is null)
            {
                return NotFound("Charge not found.");
            }

            var returnCharge = _mapper.Map<ChargeDto>(charge);
            return Ok(returnCharge);
        }

        [HttpDelete("{chargeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCharge(string id, string bookingId, string chargeId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var bookingExists = await _context.Bookings.AnyAsync(y => y.Id == bookingId);
            if (!bookingExists)
            {
                return NotFound("Booking not found.");
            }


            var charge = await _context.Charges.Where(x => x.Id == chargeId).FirstOrDefaultAsync();
            if (charge is null)
            {
                return NotFound("Charge not found.");
            }

            _context.Charges.Remove(charge);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}