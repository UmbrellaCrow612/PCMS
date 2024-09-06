using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PCMS.API.Models;
using PCMS.API.DTOS;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling user authentication-related actions.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthenticationController(UserManager<ApplicationUser> userManager, ILogger<AuthenticationController> logger) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<AuthenticationController> _logger = logger;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The DTO containing user registration information.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!_userManager.SupportsUserEmail)
                {
                    _logger.LogError("UserManager does not support email");
                    return StatusCode(StatusCodes.Status501NotImplemented, new ErrorResponse { Errors = ["Email support is not implemented."] });
                }

                _logger.LogInformation("Register request received for {Email}", request.Email);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for {Email}", request.Email);
                    return BadRequest(new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred.",
                        Detail = "Please refer to the errors property for additional details.",
                        Instance = HttpContext.Request.Path
                    });
                }

                var user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Username ?? request.Email, // Fallback to email if username is not provided
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Rank = request.Rank,
                    BadgeNumber = request.BadgeNumber,
                    DOB = request.DOB,
                    PhoneNumber = request.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to register user {Email}. Errors: {Errors}",
                        request.Email, string.Join(", ", result.Errors.Select(e => e.Description)));

                    return BadRequest(new ErrorResponse
                    {
                        Errors = result.Errors.Select(e => e.Description).ToArray()
                    });
                }

                _logger.LogInformation("User {Email} registered successfully", request.Email);

                return Ok(new SuccessResponse
                {
                    Message = "User registered successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user {Email}", request.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Errors = ["An unexpected error occurred. Please try again later."]
                });
            }
        }

        private class SuccessResponse
        {
            public string Message { get; set; } = string.Empty;
        }

        private class ErrorResponse
        {
            public string[] Errors { get; set; } = [];
        }


    }
}
