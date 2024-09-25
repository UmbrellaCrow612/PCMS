using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.Dtos.POST;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Handle charge related actions
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("persons/{id}/bookings/{bookingId}/charges")]
    public class ChargeController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateCharge(string id, string bookingId, [FromBody] POSTCharge request)
        {
            return Ok();
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
