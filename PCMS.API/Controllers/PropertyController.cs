using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling property-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PropertyController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    [ApiController]
    [Route("properties")]
    [Authorize]
    public class PropertyController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="request">The DTO containing POST property information.</param>
        /// <returns>The created property details.</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyDto request)
        {
            var location = await _context.Locations.Where(l => l.Id == request.LocationId).FirstOrDefaultAsync();
            if (location is null)
            {
                return NotFound("Location not found");
            }

            var property = _mapper.Map<Property>(request);

            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            var _property = await _context.Properties.Where(p => p.Id == property.Id)
                .Include(p => p.Location)
                .FirstOrDefaultAsync() ?? throw new Exception("Could not fetch the property just made");

            var returnProperty = _mapper.Map<PropertyDto>(_property);

            return CreatedAtAction(nameof(CreateProperty), new { id = returnProperty.Id }, returnProperty);
        }

        /// <summary>
        /// Get a property.
        /// </summary>
        /// <param name="id">The ID of the property.</param>
        /// <returns>The property details.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PropertyDto>> GetProperty(string id)
        {
            var property = await _context.Properties.Where(p => p.Id == id).Include(p => p.Location).FirstOrDefaultAsync();
            if (property is null)
            {
                return NotFound("Property not found");
            }

            var returnProperty = _mapper.Map<PropertyDto>(property);
            return Ok(returnProperty);
        }

        /// <summary>
        /// Patch a property.
        /// </summary>
        /// <param name="id">The ID of the property.</param>
        /// <returns>No content.</returns>
        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchProperty(string id, [FromBody] UpdatePropertyDto request)
        {
            var property = await _context.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (property is null)
            {
                return NotFound("Property not found.");
            }

            var location = await _context.Locations.Where(l => l.Id == request.LocationId).FirstOrDefaultAsync();
            if (location is null)
            {
                return NotFound("Location not found");
            }

            _mapper.Map(request, property);
            property.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete a property.
        /// </summary>
        /// <param name="id">The ID of the property.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteProperty(string id)
        {
            var property = await _context.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (property is null)
            {
                return NotFound("Property not found.");
            }

            _context.Remove(property);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}