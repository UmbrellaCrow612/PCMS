using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.PATCH;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("vehicles")]
    public class VehicleController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateVehicle([FromBody] POSTVehicle request)
        {
            var vehicle = _mapper.Map<Vehicle>(request);

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            var returnVehicle = _mapper.Map<VehicleDto>(vehicle);
            return CreatedAtAction(nameof(CreateVehicle), new { id = returnVehicle.Id }, returnVehicle);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetVehicle(string id)
        {
            var vehicle = await _context.Vehicles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (vehicle is null)
            {
                return NotFound("Vehicle not found.");
            }

            var returnVehicle = _mapper.Map<VehicleDto>(vehicle);
            return Ok(returnVehicle);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateVehicle(string id, [FromBody] PATCHVehicle request)
        {
            var vehicle = await _context.Vehicles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (vehicle is null)
            {
                return NotFound("Vehicle not found.");
            }

            _mapper.Map(request, vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteVehicle(string id)
        {
            var vehicle = await _context.Vehicles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (vehicle is null)
            {
                return NotFound("Vehicle not found.");
            }

            _context.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/cases/{caseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> LinkVehicleToCase(string id, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var vehicleExists = await _context.Vehicles.AnyAsync(x => x.Id == id);
            if (!vehicleExists)
            {
                return NotFound("Vehicle not found.");
            }

            var linkExists = await _context.CaseVehicles.Where(x => x.VehicleId == id && x.CaseId == caseId).FirstOrDefaultAsync();
            if (linkExists is not null)
            {
                return BadRequest("Vehicle is already linked to this case");
            }
            
            var link = new CaseVehicle
            {
                CaseId = caseId,
                VehicleId = id
            };

            await _context.CaseVehicles.AddAsync(link);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}/cases/{caseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UnLinkVehicleToCase(string id, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var vehicleExists = await _context.Vehicles.AnyAsync(x => x.Id == id);
            if (!vehicleExists)
            {
                return NotFound("Vehicle not found.");
            }

            var link = await _context.CaseVehicles.Where(x => x.VehicleId == id && x.CaseId == caseId).FirstOrDefaultAsync();
            if (link is null)
            {
                return BadRequest("Vehicle is not linked to this case");
            }

            _context.CaseVehicles.Remove(link);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
