using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;
using System.Diagnostics;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController(UserManager<ApplicationUser> userManager, IMapper mapper) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        [HttpPost("register")]
        public async Task<Results<Ok, ValidationProblem>> Register([FromBody] PCMSRegisterRequest request)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }
            return TypedResults.Ok();
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