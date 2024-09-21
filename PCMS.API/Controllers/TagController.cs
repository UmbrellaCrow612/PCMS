using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;
using PCMS.API.Dtos.PATCH;

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
        public async Task<ActionResult<GETTag>> CreateTag([FromBody] POSTTag request)
        {
            var tag = _mapper.Map<Tag>(request);

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            var returnTag = _mapper.Map<GETTag>(tag);
            return Ok(returnTag);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GETTag>> GetTag(string id)
        {
            var tag = await _context.Tags.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tag is null)
            {
                return NotFound("Tag not found.");
            }

            var returnTag = _mapper.Map<GETTag>(tag);
            return Ok(returnTag);
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
    }
}
