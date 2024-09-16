using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS;
using PCMS.API.Models;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling person-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PersonController"/> class.
    /// </remarks>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">AutoMapper instance</param>
    [ApiController]
    [Route("persons")]
    [Authorize]
    public class PersonController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="request">DTO for POST data for a person.</param>
        /// <returns>The created person</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETPerson>> CreatePerson([FromBody] POSTPerson request)
        {
            var person = _mapper.Map<Person>(request);

            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            var returnPerson = _mapper.Map<GETPerson>(person);

            return CreatedAtAction(nameof(CreatePerson), new { id = returnPerson.Id }, returnPerson);
        }
    }
}
