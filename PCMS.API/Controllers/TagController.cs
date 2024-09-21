using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.Dtos.GET;
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
        public async Task<ActionResult<GETTag>> CreateTag([FromBody] POSTTag request)
        {
            var tag = _mapper.Map<Tag>(request);

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<GETTag>(tag));
        }
    }
}
