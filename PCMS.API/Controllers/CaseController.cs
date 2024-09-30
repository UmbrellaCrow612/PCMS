using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic;
using PCMS.API.Dtos.GET;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Filters;
using PCMS.API.Models;
using PCMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling case-related actions.
    /// </summary>
    [ApiController]
    [Route("cases")]
    [Authorize]
    public class CaseController(ApplicationDbContext context, IMapper mapper, ICaseService caseService) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICaseService _caseService = caseService;


        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETCase>> CreateCase([FromBody] POSTCase request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var _case = await _caseService.CreateCaseAsync(request, userId);

            return CreatedAtAction(nameof(CreateCase), new { id = _case.Id }, _case);
        }



        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETCase>> GetCase(string id)
        {
            var result = await _caseService.GetCaseByIdAsync(id);

            if (result is null)
            {
                return NotFound("Case not found");
            }

            return Ok(result);
        }


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
                [FromQuery] string? createdById,
                [FromQuery] string? departmentId,
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

            if (!string.IsNullOrEmpty(createdById))
                query = query.Where(c => c.CreatedById == createdById);

            if (!string.IsNullOrEmpty(departmentId))
                query = query.Where(c => c.DepartmentId == departmentId);

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

            var result = await _caseService.UpdateCaseByIdAsync(id, userId, request);

            if (result is null)
            {
                return NotFound("Case not found");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCase(string id)
        {
            var isDeleted = await _caseService.DeleteCaseByIdAsync(id);
            if (!isDeleted)
            {
                return NotFound("Case to delete not found");
            }

            return NoContent();
        }


        [HttpGet("{id}/persons")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETPerson>>> GetCasePersons(string id)
        {
            var persons = await _caseService.GetCasePersonsAsync(id);

            return Ok(persons);
        }


        [HttpGet("{id}/users")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETApplicationUser>>> GetUserCases(string id)
        {
            var users = await _caseService.GetCaseUsersAsync(id);

            return Ok(users);
        }



        [HttpGet("{id}/edits")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETCaseEdit>>> GetCaseEdits(string id)
        {
            var edits = await _caseService.GetCaseEditsByIdAsync(id);

            return Ok(edits);
        }

        [HttpGet("{id}/tags")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETTag>>> GetCaseTags(string id)
        {
           var tags = await _caseService.GetCaseTagsAsync(id);

            return Ok(tags);

        }
    }
}