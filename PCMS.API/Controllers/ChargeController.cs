using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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
        public async Task<ActionResult> CreateCharge(string id, string bookingId, [FromBody] POSTCharge request)
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

            var returnCharge = _mapper.Map<GETCharge>(charge);

            return CreatedAtAction(nameof(CreateCharge), returnCharge);
        }

        [HttpGet]
        public async Task<ActionResult> GetCharges(string id, string bookingId)
        {
            return Ok();
        }

        [HttpGet("{chargeId}")]
        public async Task<ActionResult> GetCharge(string id, string bookingId, string chargeId)
        {
            return Ok();
        }

        [HttpDelete("{chargeId}")]
        public async Task<ActionResult> DeleteCharge(string id, string bookingId, string chargeId)
        {
            return Ok();
        }
    }
}
