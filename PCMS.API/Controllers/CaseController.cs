using Microsoft.AspNetCore.Mvc;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("cases")]
    public class CaseController(ILogger<CaseController> logger) : ControllerBase
    {
        private readonly ILogger<CaseController> _logger = logger;

        /// <summary>
        /// Creates a new case with the provided pet information.
        /// </summary>
        /// <param name="pet">The information about the pet.</param>
        /// <returns>The newly created case.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreateACase([FromBody] string pet)
        {
            if (string.IsNullOrWhiteSpace(pet))
            {
                _logger.LogWarning("Attempt to create a case with empty pet information.");
                return BadRequest("Pet information cannot be empty.");
            }

            try
            {
                // TODO: Replace this with actual case creation logic.
                var newCaseId = await Task.FromResult("newCaseId");

                _logger.LogInformation("Case created successfully with ID: {CaseId}", newCaseId);
                return CreatedAtAction(nameof(GetCase), new { id = newCaseId }, new { id = newCaseId, pet });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a case.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the case.");
            }
        }

        /// <summary>
        /// Retrieves a case by its ID.
        /// </summary>
        /// <param name="id">The ID of the case.</param>
        /// <returns>The requested case.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> GetCase(string id)
        {
            // TODO: Implement logic to retrieve a case by id
            var caseData = await Task.FromResult($"Case with id {id}");

            if (caseData == null)
            {
                _logger.LogWarning("Case with ID {CaseId} not found.", id);
                return NotFound($"Case with id {id} not found.");
            }

            _logger.LogInformation("Case with ID {CaseId} retrieved successfully.", id);
            return Ok(caseData);
        }
    }
}
