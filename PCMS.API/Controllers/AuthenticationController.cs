using AutoMapper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.Auth;
using PCMS.API.Dtos.Create;
using PCMS.API.Models;
using System.Diagnostics;

namespace PCMS.API.Controllers
{
    /// <summary>
    /// Baseed on ASP.NET identity MapIdentity routes
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="mapper"></param>
    [ApiController]
    [Route("auth")]
    public class AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager ,IMapper mapper, RoleManager<IdentityRole> rolesManager) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly RoleManager<IdentityRole> _rolesManager = rolesManager;

        [HttpPost("register")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<Results<Ok,BadRequest, ValidationProblem>> Register([FromBody] CreateRegisterRequestDto request)
        {
            var roleExists = await _rolesManager.RoleExistsAsync(request.Role);
            if (!roleExists)
            {
                return TypedResults.BadRequest();
            }

            var user = _mapper.Map<ApplicationUser>(request);

            var userResult = await _userManager.CreateAsync(user, request.Password);
            if (!userResult.Succeeded)
            {
                return CreateValidationProblem(userResult);
            }

            await _userManager.AddToRoleAsync(user, request.Role);

            return TypedResults.Ok();
        }

        [HttpPost("login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] CreateLoginRequestDto request, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
        {
            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);

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
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            return TypedResults.Empty;
        }

        private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
            TypedResults.ValidationProblem(new Dictionary<string, string[]> {
                { errorCode, [errorDescription] }
            });

        private static ValidationProblem CreateValidationProblem(IdentityResult result)
        {
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