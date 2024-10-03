using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("tags")]
    [Authorize]
    public class TagController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<TagDto>> CreateTag([FromBody] POSTTag request)
        {
            var tag = _mapper.Map<Tag>(request);

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            var returnTag = _mapper.Map<TagDto>(tag);
            return Ok(returnTag);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TagDto>> GetTag(string id)
        {
            var tag = await _context.Tags.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tag is null)
            {
                return NotFound("Tag not found.");
            }

            var returnTag = _mapper.Map<TagDto>(tag);
            return Ok(returnTag);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<TagDto>>> SearchTags([FromQuery] string name)
        {
            var tags = await _context.Tags
                .Where(t => t.Name.Contains(name))
                .ToListAsync();

            var returnTags = _mapper.Map<IEnumerable<TagDto>>(tags);
            return Ok(returnTags);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTag(string id, [FromBody] PATCHTag request)
        {
            var tag = await _context.Tags.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tag is null)
            {
                return NotFound("Tag not found.");
            }

            _mapper.Map(request, tag);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTag(string id)
        {
            var tag = await _context.Tags.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tag is null)
            {
                return NotFound("Tag not found.");
            }

            var caseTags = await _context.CaseTags.Where(ct => ct.TagId == id).ToListAsync();

            if (caseTags.Count is not 0)
            {
                return BadRequest("Tag is associated with cases through case tags.");
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/cases/{caseId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateCaseTags(string id, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var tagExists = await _context.Tags.AnyAsync(t => t.Id == id);
            if (!tagExists)
            {
                return NotFound("Tag not found.");
            }

            var caseTagExists = await _context.CaseTags.AnyAsync(_ => _.CaseId == caseId && _.TagId == id);
            if (caseTagExists)
            {
                return BadRequest("Tag is already linked to this case");
            }

            var caseTag = new CaseTag
            {
                CaseId = caseId,
                TagId = id
            };

            await _context.CaseTags.AddAsync(caseTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}/cases/{caseId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCaseTag(string id, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return NotFound("Case not found.");
            }

            var tagExists = await _context.Tags.AnyAsync(t => t.Id == id);
            if (!tagExists)
            {
                return NotFound("Tag not found.");
            }

            var caseTag = await _context.CaseTags.Where(ct => ct.CaseId == caseId && ct.TagId == id).FirstOrDefaultAsync();
            if (caseTag is null)
            {
                return BadRequest("Case tag dose not exist.");
            }

            _context.CaseTags.Remove(caseTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}