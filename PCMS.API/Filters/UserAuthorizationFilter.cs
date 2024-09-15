using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PCMS.API.Models;
using System.Security.Claims;

namespace PCMS.API.Filters
{
    /// <summary>
    /// Custom Filter to make sure the user exists, so in a method when accessing the underlying User we know it is valid.
    /// </summary>
    public class UserAuthorizationFilter(UserManager<ApplicationUser> userManager, ILogger<UserAuthorizationFilter> logger) : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<UserAuthorizationFilter> _logger = logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Unauthorized attempt to access resource.");
                context.Result = new UnauthorizedObjectResult("Unauthorized");
                return;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                context.Result = new UnauthorizedObjectResult("Unauthorized");
                return;
            }

            await next();
        }
    }
}