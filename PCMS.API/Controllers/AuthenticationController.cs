using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;
using System.Diagnostics;


namespace PCMS.API.Controllers
{
    /// <summary>
    /// Controller for handling user authentication-related actions. Based on app.MapIdentityApi<ApplicationUser>();
    /// </summary>
    /// 
    [ApiController]
    [Route("auth")]
    public class AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthenticationController> logger) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ILogger<AuthenticationController> _logger = logger;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The DTO containing user registration information.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok, ValidationProblem>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!_userManager.SupportsUserEmail)
                {
                    _logger.LogError("UserManager does not support email");

                    throw new NotSupportedException($"{nameof(AuthenticationController)} requires a user store with email support.");
                }

                _logger.LogInformation("Register request received for {Email}", request.Email);

                var user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Username ?? request.Email, // Fallback to email if username is not provided
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Rank = request.Rank,
                    BadgeNumber = request.BadgeNumber,
                    DateOfBirth = request.DateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to register user {Email}. Errors: {Errors}",
                        request.Email, string.Join(", ", result.Errors.Select(e => e.Description)));

                    return CreateValidationProblem(result);
                }

                _logger.LogInformation("User {Email} registered successfully", request.Email);

                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user {Email}", request.Email);

                return CreateValidationProblem(IdentityResult.Failed());
            }
        }


        /// <summary>
        /// Login a user.
        /// </summary>
        /// <param name="request">DTO for login request body</param>
        /// <param name="useCookies">Query string if a client wants to use cookie based auth</param>
        /// <param name="useSessionCookies">Query string if a client wants to use session cookies based auth</param>
        /// <returns>Success or Failure</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] LoginRequest request, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
        {
            try
            {
                var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
                var isPersistent = (useCookies == true) && (useSessionCookies != true);

                _logger.LogInformation("Login request received for {UserName} with auth settings {useCookieScheme} is persistent {isPersistent}", request.UserName, useCookieScheme, isPersistent);

                _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

                var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent, lockoutOnFailure: true);

                if (result.RequiresTwoFactor)
                {
                    _logger.LogInformation("Login request for username: {UserName} has TFA enabled", request.UserName);

                    if (!string.IsNullOrEmpty(request.TwoFactorCode))
                    {
                        _logger.LogInformation("Login request for username: {UserName} TFA sign in attempt", request.UserName);

                        result = await _signInManager.TwoFactorAuthenticatorSignInAsync(request.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                    }
                    else if (!string.IsNullOrEmpty(request.TwoFactorRecoveryCode))
                    {
                        _logger.LogInformation("Login request for username: {UserName} TFRC sign in attempt", request.UserName);

                        result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(request.TwoFactorRecoveryCode);
                    }
                }

                if (!result.Succeeded)
                {
                    _logger.LogInformation("Login request attempt failed for {UserName}", request.UserName);

                    return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
                }

                _logger.LogInformation("Login request for {UserName} successful", request.UserName);

                return TypedResults.Empty;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Logging in the user {UserName}", request.UserName);

                return TypedResults.Problem(statusCode: StatusCodes.Status500InternalServerError);
            }


        }

        private static ValidationProblem CreateValidationProblem(IdentityResult result)
        {
            // We expect a single error code and description in the normal case.
            // This could be golfed with GroupBy and ToDictionary, but perf! :P
            Debug.Assert(!result.Succeeded);
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            return TypedResults.ValidationProblem(errorDictionary);
        }



    }
}