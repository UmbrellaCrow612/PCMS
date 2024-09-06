using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PCMS.API.Models;
using PCMS.API.DTOS;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using Asp.Versioning;


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

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for {Email}", request.Email);

                    var errors = ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => new IdentityError
                                           {
                                               Code = "InvalidModelState",
                                               Description = e.ErrorMessage
                                           })
                                            .ToArray();

                    return CreateValidationProblem(IdentityResult.Failed(errors));
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
        /// <param name="request"></param>
        /// <param name="useCookies"></param>
        /// <param name="useSessionCookies"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

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
                    if (!string.IsNullOrEmpty(request.TwoFactorCode))
                    {
                        result = await _signInManager.TwoFactorAuthenticatorSignInAsync(request.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                    }
                    else if (!string.IsNullOrEmpty(request.TwoFactorRecoveryCode))
                    {
                        result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(request.TwoFactorRecoveryCode);
                    }
                }

                if (!result.Succeeded)
                {
                    _logger.LogInformation("Login request faild for {UserName}", request.UserName);

                    return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
                }

                return TypedResults.Empty;

            }
            catch (Exception)
            {

                throw;
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
