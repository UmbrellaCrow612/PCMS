using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;

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
        public async Task<ActionResult<GETProperty>> CreateProperty([FromBody] POSTProperty request)
        {
            var location = await _context.Locations.Where(l => l.Id == request.LocationId).FirstOrDefaultAsync();
            if (location == null)
            {
                return NotFound("Location not found");
            }

            var property =  _mapper.Map<Property>(request);

            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            var _property = await _context.Properties.Where(p => p.Id == property.Id)
                .Include(p => p.Location)
                .FirstOrDefaultAsync() ?? throw new Exception("Could not fetch the property just made");

            var returnProperty = _mapper.Map<GETProperty>(_property);

            return CreatedAtAction(nameof(CreateProperty), new { id = returnProperty.Id }, returnProperty);
        }
    }
}