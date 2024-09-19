using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PCMS.API.DTOS.POST;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.GET;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CaseController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    [ApiController]
    [Route("cases")]
    [Authorize]
    public class CaseController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new case.
        /// </summary>
        /// <param name="request">The DTO containing POST case information.</param>
        /// <returns>The created case details.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETCase>> CreateCase([FromBody] POSTCase request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var newCase = _mapper.Map<Case>(request);
            newCase.CreatedById = userId;
            newCase.LastEditedById = userId;

            await _context.Cases.AddAsync(newCase);
            await _context.SaveChangesAsync();

            var createdCase = await _context.Cases
                .FirstOrDefaultAsync(c => c.Id == newCase.Id)
                ?? throw new ApplicationException("Failed to retrieve the created case");

            var returnCase = _mapper.Map<GETCase>(createdCase);

            return CreatedAtAction(nameof(CreateCase), new { id = returnCase.Id }, returnCase);
        }



        /// <summary>
        /// Retrieves a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>The requested case.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETCase>> GetCase(string id)
        {
            var caseEntity = await _context.Cases
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (caseEntity is null)
            {
                return NotFound("Case not found.");
            }

            var caseResult = _mapper.Map<GETCase>(caseEntity);

            return Ok(caseResult);
        }

        /// <summary>
        /// Retrieves all cases.
        /// </summary>
        /// <returns>A list of all cases.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETCase>>> GetCases()
        {
            var cases = await _context.Cases
                .ToListAsync();

            if (cases.Count is 0)
            {
                return Ok(new List<GETCase>());
            }

            var returnCases = _mapper.Map<List<GETCase>>(cases);

            return Ok(returnCases);
        }

        /// <summary>
        /// Updates a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case to update.</param>
        /// <param name="request">The DTO containing the updated case information.</param>
        /// <returns>No content if successful.</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserAuthorizationFilter))]
        public async Task<ActionResult> PatchCase(string id, [FromBody] PATCHCase request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var existingCase = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCase is null)
            {
                return NotFound("Case not found.");
            }

            _mapper.Map(request, existingCase);
            existingCase.LastEditedById = userId;
            existingCase.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCase(string id)
        {
            var caseToDelete = await _context.Cases.FindAsync(id);

            if (caseToDelete is null)
            {
                return NotFound("Case not found");
            }

            _context.Remove(caseToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all persons linked to a case through case person.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>List of people linked to the case through case person.</returns>
        [HttpGet("{id}/persons")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETPerson>>> GetCasePersons(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Get all users linked to a case through app user case.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>List of users linked to the case through app user case.</returns>
        [HttpGet("{id}/users")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETApplicationUser>>> GetUserCases(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Links a person to a case.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="request">The DTO of the POST data for linking a person.</param>
        /// <returns>No content.</returns>
        [HttpPost("{id}/persons/{personId}/link")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateCasePerson(string id, string personId, POSTCasePerson request)
        {
            var existingCase = await _context.Cases.AnyAsync(c => c.Id == id);
            if (!existingCase)
            {
                return NotFound("Case not found");
            }

            var existingPerson = await _context.Persons.AnyAsync(p => p.Id == personId);
            if (!existingPerson)
            {
                return NotFound("Person not found.");
            }

            var casePerson = new CasePerson
            {
                CaseId = id,
                PersonId = personId,
                Role = request.Role
            };

            await _context.CasePersons.AddAsync(casePerson);
            await _context.SaveChangesAsync();

            var returnCasePerson = _mapper.Map<GETCasePerson>(casePerson);

            return Ok(returnCasePerson);
        }

        /// <summary>
        /// Deletes a person case link.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <param name="personId">The ID of the person.</param>
        /// <param name="linkdId">The ID of the person case</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}/persons/{personId}/link/{linkId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCasePerson(string id, string personId, string linkId)
        {
            var casePerson = await _context.CasePersons
                .Where(cp => cp.CaseId == id && cp.PersonId == personId && cp.Id == linkId)
                .FirstOrDefaultAsync();

            if (casePerson is null)
            {
                return NotFound("Case person link not found");
            }

            _context.Remove(casePerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Creates a application user case which is used as a ref to check if a user is assigned to a given case.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <param name="userId">The ID of the app user i.e officer NOT a person.</param>
        /// <returns>No content</returns>
        [HttpPost("{id}/users/{userId}/assign")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateUserCase(string id, string userId)
        {
            var caseExists = await _context.Cases.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (caseExists is null)
            {
                return NotFound("Case not found");
            }

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user is null)
            {
                return NotFound("User not found");
            }

            var existingLink = await _context.ApplicationUserCases.Where(auc => auc.CaseId == id && auc.UserId == userId).FirstOrDefaultAsync();

            if (existingLink is not null)
            {
                return BadRequest("User link already exists");
            }

            var applicationUserCase = new ApplicationUserCase
            {
                CaseId = id,
                UserId = userId,
            };

            await _context.ApplicationUserCases.AddAsync(applicationUserCase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a application user case.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <param name="userId">The ID of the app user i.e officer NOT a person.</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}/users/{userId}/assign")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUserCase(string id, string userId)
        {
            var existingLink = await _context.ApplicationUserCases
                .Where(auc => auc.CaseId == id && auc.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingLink is null)
            {
                return BadRequest("User link dose not exist");
            }

            _context.Remove(existingLink);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}