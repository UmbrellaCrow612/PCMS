﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PCMS.API.Models;
using PCMS.API.DTOS;
using System.ComponentModel.DataAnnotations;

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
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">The registration request containing user information.</param>
        /// <returns>Returns a success message or error response if registration fails.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!_userManager.SupportsUserEmail)
                {
                    throw new NotSupportedException($"{nameof(AuthenticationController)} requires a user store with email support.");
                }

                _logger.LogInformation("Register request received for {Email}", request.Email);

                var email = request.Email;

                if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
                {
                    _logger.LogWarning("Invalid model state for {Email}", request.Email);
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Detail = "Invalid email address or empty email.",
                        Instance = HttpContext.Request.Path
                    };
                    return BadRequest(problemDetails);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for {Email}", request.Email);
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Detail = "Model state is invalid.",
                        Instance = HttpContext.Request.Path
                    };
                    return BadRequest(problemDetails);
                }

                var user = new ApplicationUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Rank = request.Rank,
                    BadgeNumber = request.BadgeNumber,
                    DOB = request.DOB,
                    UserName = request.Username,
                    PhoneNumber = request.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to register user {Email}. Errors: {Errors}",
                        request.Email, string.Join(", ", result.Errors.Select(e => e.Description)));

                    // Convert IdentityResult errors to a custom error response model
                    var errorResponse = new
                    {
                        Errors = result.Errors.Select(e => e.Description)
                    };
                    return BadRequest(errorResponse);
                }

                _logger.LogInformation("User {Email} registered successfully", request.Email);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user {Email}", request.Email);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
