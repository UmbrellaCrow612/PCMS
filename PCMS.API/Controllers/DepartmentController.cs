using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.DTOS.GET;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.PATCH;

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
    }
}