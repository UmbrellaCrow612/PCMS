using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.Security.Claims;

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

            _context.Cases.Add(newCase);
            await _context.SaveChangesAsync();

            var createdCase = await _context.Cases
                .FirstOrDefaultAsync(c => c.Id == newCase.Id)
                ?? throw new ApplicationException("Failed to retrieve the created case");

            var returnCase = _mapper.Map<GETCase>(createdCase);

            return CreatedAtAction(nameof(GetCase), new { id = returnCase.Id }, returnCase);
        }



        /// <summary>
        /// Retrieves a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>The requested case.</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETCase>> GetCase([FromRoute] string id)
        {
            var caseEntity = await _context.Cases
                .Include(c => c.CaseActions)
                .Include(c => c.AssignedUsers)
                .Include(c => c.Reports)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (caseEntity is null)
            {
                return NotFound($"Case with ID '{id}' was not found.");
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
                .Include(c => c.CaseActions)
                .Include(c => c.AssignedUsers)
                .Include(c => c.Reports)
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
        public async Task<ActionResult> PatchCase([FromRoute] string id, [FromBody] PATCHCase request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var existingCase = await _context.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCase is null)
            {
                return NotFound("Case not found.");
            }

            existingCase.Title = request.Title;
            existingCase.Description = request.Description;
            existingCase.Status = request.Status;
            existingCase.Priority = request.Priority;
            existingCase.Type = request.Type;
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
        public async Task<IActionResult> DeleteCase([FromRoute] string id)
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
    }
}