using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("tags")]
    [Authorize]
    public class TagController(ITagService tagService) : ControllerBase
    {
        private readonly ITagService _tagService = tagService;

        [HttpPost]
        [ServiceFilter(typeof(UserValidationFilter))]
        public async Task<ActionResult<TagDto>> CreateTag([FromBody] CreateTagDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var tag = await _tagService.CreateTagAsync(userId, request);

            return Ok(tag);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TagDto>> GetTag(string id)
        {
           var tag = await _tagService.GetTagByIdAsync(id);
            if (tag is null) return NotFound("Tag not found.");

            return Ok(tag);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTag(string id, [FromBody] UpdateTagDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var isUpdated = await _tagService.UpdateTagByIdAsync(id, userId, request);
            if (!isUpdated) return NotFound("Tag not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTag(string id)
        {
            var isDeleted = await _tagService.DeleteTagByIdAsync(id);
            if (!isDeleted) return BadRequest("Tag not found or is linked to a case.");

            return NoContent();
        }

        [HttpPost("{id}/cases/{caseId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateCaseTags(string id, string caseId)
        {
           var linked = await _tagService.LinkTagToCase(id, caseId);
            if (!linked) return BadRequest("Tag or case dose not exist or is already linked to this case.");

            return NoContent();
        }


        [HttpDelete("{id}/cases/{caseId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCaseTag(string id, string caseId)
        {
            var unLinked = await _tagService.UnLinkTagFromCase(id, caseId);
            if (!unLinked) return BadRequest("Case or tag dose not exist or they are not linked already.");

            return NoContent();
        }
    }
}