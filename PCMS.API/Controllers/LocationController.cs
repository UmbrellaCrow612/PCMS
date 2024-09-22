using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling location-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocationController"/> class.
    /// </remarks>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">Auto Mapper</param>
    [ApiController]
    [Route("locations")]
    [Authorize]
    public class LocationController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="request">The DTO containing POST location information.</param>
        /// <returns>The location details.</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETLocation>> CreateLocation([FromBody] POSTLocation request)
        {
            var location = _mapper.Map<Location>(request);

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            var returnLocation = _mapper.Map<GETLocation>(location);
            return CreatedAtAction(nameof(CreateLocation), new { id = returnLocation.Id }, returnLocation);
        }


        /// <summary>
        /// Get a location.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <returns>The location details.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETLocation>> GetLocation(string id)
        {
            var location = await _context.Locations.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (location is null)
            {
                return NotFound("Location not found.");
            }

            var returnLocation = _mapper.Map<GETLocation>(location);
            return Ok(returnLocation);
        }

        /// <summary>
        /// Patch a location.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <returns>No content.</returns>
        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchLocation(string id, [FromBody] PATCHLocation request)
        {
            var location = await _context.Locations.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (location is null)
            {
                return NotFound("Location not found");
            }

            _mapper.Map(request, location);
            location.LastModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a location.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteLocation(string id)
        {
            var location = await _context.Locations.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (location is null)
            {
                return NotFound("Location not found");
            }

            _context.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}