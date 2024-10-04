using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.POST;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
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
    [AllowAnonymous]
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
        public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] CreatePersonDto request)
        {
            var person = _mapper.Map<Person>(request);

            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            var returnPerson = _mapper.Map<PersonDto>(person);

            return CreatedAtAction(nameof(CreatePerson), new { id = returnPerson.Id }, returnPerson);
        }

        /// <summary>
        /// Get a person
        /// </summary>
        /// <param name="id">The ID of the person</param>
        /// <returns>A person</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDto>> GetPerson(string id)
        {
            var person = await _context.Persons.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (person is null)
            {
                return NotFound("Person not found");
            }

            var returnPerson = _mapper.Map<PersonDto>(person);
            return Ok(returnPerson);
        }

        /// <summary>
        /// Patch a person.
        /// </summary>
        /// <param name="id">The ID of the person.</param>
        /// <param name="request">The DTO of the POST data of the person.</param>
        /// <returns>No Content</returns>
        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchPerson(string id, [FromBody] UpdatePersonDto request)
        {
            var person = await _context.Persons.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (person is null)
            {
                return NotFound("Person not found");
            }

            _mapper.Map(request, person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a person.
        /// </summary>
        /// <param name="id">The ID of the person.</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePerson(string id)
        {
            var person = await _context.Persons.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (person is null)
            {
                return NotFound("Person not found");
            }

            var casePeople = await _context.CasePersons.Where(cp => cp.PersonId == id).ToListAsync();

            _context.Remove(person);
            _context.Remove(casePeople);

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost("{id}/cases/{caseId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateCasePerson(string id, string caseId, CreateCasePersonDto request)
        {
            var existingCase = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!existingCase)
            {
                return NotFound("Case not found");
            }

            var existingPerson = await _context.Persons.AnyAsync(p => p.Id == id);
            if (!existingPerson)
            {
                return NotFound("Person not found.");
            }

            var casePerson = new CasePerson
            {
                CaseId = caseId,
                PersonId = id,
                Role = request.Role
            };

            await _context.CasePersons.AddAsync(casePerson);
            await _context.SaveChangesAsync();

            var _casePerson = await _context.CasePersons
                .Where(c => c.Id == casePerson.Id)
                .Include(c => c.Person)
                .FirstOrDefaultAsync() ?? throw new Exception("Failed to get created case person link.");

            var returnCasePerson = _mapper.Map<CasePersonDto>(_casePerson);

            return Ok(returnCasePerson);
        }


        [HttpDelete("{id}/cases/{caseId}/links/{linkId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCasePerson(string id, string caseId, string linkId)
        {
            var casePerson = await _context.CasePersons
                .Where(cp => cp.CaseId == caseId && cp.PersonId == id && cp.Id == linkId)
                .FirstOrDefaultAsync();

            if (casePerson is null)
            {
                return NotFound("Case person link not found");
            }

            _context.Remove(casePerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/crime-scenes/{crimeSceneId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> LinkPersonToCrimeScene(string id, string crimeSceneId, [FromBody] CreateCrimeScenePersonDto request)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var crimeSceneExists = await _context.CrimeScenes.AnyAsync(x => x.Id == crimeSceneId);
            if (!crimeSceneExists)
            {
                return NotFound("Crime scene not found");
            }

            var crimeScenePerson = new CrimeScenePerson
            {
                CrimeSceneId = crimeSceneId,
                PersonId = id,
                Role = request.Role
            };

            await _context.CrimeScenePersons.AddAsync(crimeScenePerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}/crime-scenes/{crimeSceneId}/links/{linkId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UnLinkPersonToCrimeScene(string id, string crimeSceneId, string linkId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == id);
            if (!personExists)
            {
                return NotFound("Person not found.");
            }

            var crimeSceneExists = await _context.CrimeScenes.AnyAsync(x => x.Id == crimeSceneId);
            if (!crimeSceneExists)
            {
                return NotFound("Crime scene not found");
            }

            var link = await _context.CrimeScenePersons
                .Where(x => x.Id == id && x.CrimeSceneId == crimeSceneId && x.PersonId == id)
                .FirstOrDefaultAsync();

            if (link is null)
            {
                return BadRequest("No link between this person and crime scene");
            }

            _context.CrimeScenePersons.Remove(link);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}