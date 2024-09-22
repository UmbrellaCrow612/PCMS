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
using System.ComponentModel.DataAnnotations;
using PCMS.API.Dtos.GET;

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
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETCase>> CreateCase([FromBody] POSTCase request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var newCase = _mapper.Map<Case>(request);
            newCase.CreatedById = userId;

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
                .Include(c => c.Creator)
                .Include(c => c.LastEditor)
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
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GETCase>>> GetCases(
                [FromQuery][EnumDataType(typeof(CaseStatus))] CaseStatus? status,
                [FromQuery][EnumDataType(typeof(CaseComplexity))] CaseComplexity? complexity,
                [FromQuery] DateTime? startDate,
                [FromQuery] DateTime? endDate,
                [FromQuery][EnumDataType(typeof(CasePriority))] CasePriority? priority,
                [FromQuery] string? type,
                [FromQuery] string? createdBy,
                [FromQuery] string? department,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10
            )
        {

            var query = _context.Cases.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status.Value);
            }

            if (complexity.HasValue)
            {
                query = query.Where(c => c.Complexity == complexity.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(c => c.DateOpened >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(c => c.DateOpened <= endDate.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(c => c.Priority == priority.Value);
            }

            if (!string.IsNullOrEmpty(type))
                query = query.Where(c => c.Type == type);

            if (!string.IsNullOrEmpty(createdBy))
                query = query.Where(c => c.CreatedById == createdBy);

            if (!string.IsNullOrEmpty(department))
                query = query.Where(c => c.DepartmentId == department);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var cases = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var returnCases = _mapper.Map<List<GETCase>>(cases);
            var response = new
            {
                Cases = returnCases,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page
            };

            return Ok(response);
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
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<ActionResult> PatchCase(string id, [FromBody] PATCHCase request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var existingCase = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCase is null)
            {
                return NotFound("Case not found.");
            }

            var caseEdit = _mapper.Map<CaseEdit>(existingCase);
            caseEdit.UserId = userId;
            caseEdit.CaseId = id;

            _mapper.Map(request, existingCase);
            existingCase.LastEditedById = userId;
            existingCase.LastModifiedDate = DateTime.UtcNow;

            await _context.CaseEdits.AddAsync(caseEdit);

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
        /// <param name="role">The role <see cref="CaseRole"/> of the type of link a person has to this case</param>
        /// <returns>List of people linked to the case through case person.</returns>
        [HttpGet("{id}/persons")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETPerson>>> GetCasePersons(string id, [FromQuery][EnumDataType(typeof(CaseRole))] CaseRole? role = null)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == id);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            if (role is null)
            {
                var query = await _context.CasePersons
                    .Where(cp => cp.CaseId == id)
                    .Select(cp => new GETCasePerson
                    {
                        Id = cp.Id,
                        Person = _mapper.Map<GETPerson>(cp.Person),
                        Role = cp.Role,
                        CaseId = id
                    })
                    .ToListAsync();

                return Ok(query);
            }

            var persons = await _context.CasePersons
                .Where(cp => cp.CaseId == id && cp.Role == role)
                .Select(cp => new GETCasePerson
                    {
                        Id = cp.Id,
                        Person = _mapper.Map<GETPerson>(cp.Person),
                        Role = cp.Role,
                        CaseId = id
                })
                    .ToListAsync();

            return Ok(persons);
        }

        /// <summary>
        /// Get all users linked to a case through app user case.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>List of users linked to the case through app user case.</returns>
        [HttpGet("{id}/users")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETApplicationUser>>> GetUserCases(string id)
        {
            var users = await _context.ApplicationUserCases.Where(ap => ap.CaseId == id)
                .Select(u => u.ApplicationUser)
                .ToListAsync();

            var returnUsers = _mapper.Map<List<GETApplicationUser>>(users);
            return Ok(returnUsers);
        }


        /// <summary>
        /// Gets the case edits made on a case with the user who made it.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>List of case edits</returns>
        [HttpGet("{id}/edits")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETCaseEdit>>> GetCaseEdits(string id)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == id);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var caseEdits = await _context.CaseEdits.Where(ce => ce.CaseId == id).Include(ce => ce.User).ToListAsync();

            var returnCaseEdits = _mapper.Map<List<GETCaseEdit>>(caseEdits);
            return Ok(returnCaseEdits);
        }

        [HttpGet("{id}/tags")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETTag>>> GetCaseTags(string id)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == id);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var tags = await _context.CaseTags.Where(ct => ct.CaseId == id).Include(ct => ct.Tag).Select(ct => ct.Tag).ToListAsync();

            var returnTags = _mapper.Map<List<GETTag>>(tags);

            return Ok(returnTags);

        }



    }
}