using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class UserController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;


        [HttpPost("{id}/cases/{caseId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateUserCase(string id, string caseId)
        {
            var caseExists = await _context.Cases.Where(c => c.Id == caseId).FirstOrDefaultAsync();
            if (caseExists is null)
            {
                return NotFound("Case not found");
            }

            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user is null)
            {
                return NotFound("User not found");
            }

            var existingLink = await _context.ApplicationUserCases.Where(auc => auc.CaseId == caseId && auc.UserId == id).FirstOrDefaultAsync();

            if (existingLink is not null)
            {
                return BadRequest("User link already exists");
            }

            var applicationUserCase = new ApplicationUserCase
            {
                CaseId = caseId,
                UserId = id,
            };

            await _context.ApplicationUserCases.AddAsync(applicationUserCase);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}/cases/{caseId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUserCase(string id, string caseId)
        {
            var existingLink = await _context.ApplicationUserCases
                .Where(auc => auc.CaseId == caseId && auc.UserId == id)
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