using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling department-related actions.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">Auto Mapper</param>
    [ApiController]
    [Route("departments")]
    [Authorize]
    public class DepartmentController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Create a department.
        /// </summary>
        /// <param name="request">The POST data for a department.</param>
        /// <returns>The created department</returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GETDepartment>> CreateDepartment([FromBody] POSTDepartment request)
        {
            var department = _mapper.Map<Department>(request);

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            var returnDepartment = _mapper.Map<GETDepartment>(department);
            return CreatedAtAction(nameof(CreateDepartment), new { id = returnDepartment.Id }, returnDepartment);
        }

        /// <summary>
        /// Get a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>The department</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETDepartment>> GetDepartment(string id)
        {
            var department = await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department is null)
            {
                return NotFound("Department not found");
            }

            var returnDepartment = _mapper.Map<GETDepartment>(department);
            return Ok(returnDepartment);
        }

        /// <summary>
        /// Patch a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <param name="request">The new data for the department</param>
        /// <returns>No content</returns>
        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchDepartment(string id, [FromBody] PATCHDepartment request)
        {
            var department = await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department is null)
            {
                return NotFound("Department not found");
            }

            _mapper.Map(request, department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            var department = await _context.Departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department is null)
            {
                return NotFound("Department not found");
            }

            _context.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets all users in a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>List of users.</returns>
        [HttpGet("{id}/users")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GETApplicationUser>>> GetDepartmentUsers(string id)
        {
            var department = await _context.Departments.AnyAsync(d => d.Id == id);
            if (!department)
            {
                return NotFound("Department not found");
            }

            var users = await _context.Users.Where(u => u.DepartmentId == id).ToListAsync();

            var returnUsers = _mapper.Map<List<GETApplicationUser>>(users);
            return Ok(returnUsers);
        }

        /// <summary>
        /// Assign a user to a department.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>No content.</returns>
        [HttpPost("{id}/users/{userId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AssignUser(string id, string userId)
        {
            var department = await _context.Departments.Where(d => d.Id == id).Include(d => d.AssignedUsers).FirstOrDefaultAsync();
            if (department is null)
            {
                return NotFound("Department not found");
            }

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user is null)
            {
                return NotFound("User not found.");
            }

            if (department.AssignedUsers.Any(u => u.Id == userId))
            {
                return BadRequest("User is already assigned to this department.");
            }

            department.AssignedUsers.Add(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Un-assign a user to a department.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}/users/{userId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UnassignUser(string id, string userId)
        {
            var department = await _context.Departments.Where(d => d.Id == id).Include(d => d.AssignedUsers).FirstOrDefaultAsync();
            if (department is null)
            {
                return NotFound("Department not found");
            }

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user is null)
            {
                return NotFound("User not found.");
            }

            if (!department.AssignedUsers.Any(u => u.Id == userId))
            {
                return BadRequest("User is not assigned to this user already.");
            }

            department.AssignedUsers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/cases")]
        public async Task<ActionResult> GetCases(string id)
        {
            var cases = await _context.Cases
                .Where(c => c.DepartmentId == id)
                .Include(c => c.Creator)
                .Include(c => c.LastModifiedBy).ToListAsync();

            var returnCases = _mapper.Map<List<GETCase>>(cases);
            return Ok(returnCases);
        }

        [HttpPost("{id}/cases/{caseId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> LinkCaseToDepartment(string id, string caseId)
        {
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == id);
            if (!departmentExists)
            {
                return NotFound("Department dose not exist.");
            }

            var case_ = await _context.Cases.FindAsync(caseId);
            if (case_ is null)
            {
                return NotFound("Case not found.");
            }

            if (case_.DepartmentId is not null)
            {
                return BadRequest("Case is already linked to a department.");
            }

            case_.DepartmentId = id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}/cases/{caseId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UnlinkCaseFromDepartment(string id, string caseId)
        {
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == id);
            if (!departmentExists)
            {
                return NotFound("Department dose not exist.");
            }

            var case_ = await _context.Cases.FindAsync(caseId);
            if (case_ is null)
            {
                return NotFound("Case not found.");
            }

            if (case_.DepartmentId != id)
            {
                return BadRequest("Case is not linked to the specified department.");
            }

            case_.DepartmentId = null;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}