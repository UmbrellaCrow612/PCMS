using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.DTOS.GET;

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


        [HttpPost]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GETDepartment>> CreateDepartment([FromBody] POSTDepartment request)
        {
            return Ok();
        }
    }
}