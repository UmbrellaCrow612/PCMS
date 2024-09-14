using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS;
using PCMS.API.Filters;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling evidence-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EvidenceController"/> class.
    /// </remarks>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    /// <param name="userManager">Identity service to manage users</param>
    /// <param name="mapper">Automapper instance</param>
    [ApiController]
    [Route("cases/{caseId}/evidences")]
    [Produces("application/json")]
    [Authorize]
    [ServiceFilter(typeof(UserAuthorizationFilter))]
    [ValidateRouteParameters]
    public class EvidenceController(ILogger<CaseController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<CaseController> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Creates a new evidence item for a specific case.
        /// </summary>
        /// <param name="caseId">The ID of the case.</param>
        /// <param name="request">The DTO containing POST evidence information.</param>
        /// <returns>The created evidence details.</returns>
        [HttpPost]
        public async Task<ActionResult<GETEvidence>> CreateEvidence([FromRoute][Required] string caseId, POSTEvidence request)
        {
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
