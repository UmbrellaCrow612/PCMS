using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace PCMS.API.Filters
{
    public class LoggingActionFilter(ILogger<LoggingActionFilter> logger) : IAsyncActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger = logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var actionName = context.ActionDescriptor.DisplayName;
            var request = context.ActionArguments;

            _logger.LogInformation("Action {actionName} started by user {userId} with request: {request}", actionName, userId, request);

            var executedContext = await next();

            if (executedContext.Exception is null)
            {
                _logger.LogInformation("Action {actionName} completed by user {userId}", actionName, userId);
            }
        }
    }
}