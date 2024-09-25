using Microsoft.AspNetCore.Mvc;
using PCMS.API.Dtos.POST;

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
    public class ReleaseController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateRelease(string id, string bookingId, [FromBody] POSTRelease request)
        {
            return Ok();
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
