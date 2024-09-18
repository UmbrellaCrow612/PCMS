using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.POST;

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
        public async Task<ActionResult<GETProperty>> CreateProperty([FromBody] POSTProperty request)
        {
            return Ok();
        }
    }
}