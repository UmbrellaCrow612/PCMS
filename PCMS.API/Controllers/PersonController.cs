using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;
using PCMS.API.Filters;
using System.Security.Claims;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("persons")]
    [AllowAnonymous]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService _personService = personService;

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] CreatePersonDto request)
        {
            var person = await _personService.CreatePersonAsync(request);

            return Created(nameof(CreatePerson), person);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDto>> GetPerson(string id)
        {
           var person = await _personService.GetPersonByIdAsync(id);

            return Ok(person);
        }

        [HttpPatch("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchPerson(string id, [FromBody] UpdatePersonDto request)
        {
            var updated = await _personService.UpdatePersonByIdAsync(id, request);
            if (!updated) return NotFound("Person not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ServiceFilter(typeof(UserValidationFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePerson(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var deleted = await _personService.DeletePersonByIdAsync(id, userId);
            if (!deleted) return NotFound("Person not found.");

            return NoContent();
        }


        [HttpPost("{id}/cases/{caseId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateCasePerson(string id, string caseId, CreateCasePersonDto request)
        {
            var linked = await _personService.AddPersonToCaseAsync(id, caseId, request.Role);
            if (!linked) return BadRequest("Person or case dose not exist or a link with this role already exists for this case and person.");

            return Ok();
        }


        [HttpDelete("{id}/cases/{caseId}/links/{linkId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCasePerson(string id, string caseId, string linkId)
        {
            var deleted = await _personService.DeletePersonCaseLinkAsync(id, caseId, linkId);
            if (!deleted) return BadRequest("Person or case dose not exist or a link between this person and case dose not exist.");

            return NoContent();
        }

        [HttpPost("{id}/crime-scenes/{crimeSceneId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LinkPersonToCrimeScene(string id, string crimeSceneId, [FromBody] CreateCrimeScenePersonDto request)
        {
            var linked = await _personService.AddPersonToCrimeSceneAsync(id,crimeSceneId, request.Role);
            if (!linked) return BadRequest("Person or crime scene dose not exist or person is already linked to case with the same role.");

            return Ok();
        }

        [HttpDelete("{id}/crime-scenes/{crimeSceneId}/links/{linkId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UnLinkPersonToCrimeScene(string id, string crimeSceneId, string linkId)
        {
            var deleted = await _personService.DeletePersonCrimeSceneLink(id, crimeSceneId, linkId);
            if (!deleted) return BadRequest("Person or crime scene dose not exist or the link dose not exist.");

            return NoContent();
        }

    }
}