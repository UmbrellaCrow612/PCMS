using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS;

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
        /// <returns>The location case details.</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GETLocation>> CreateLocation([FromBody] POSTLocation request)
        {
            return Ok();
        }
    }
}