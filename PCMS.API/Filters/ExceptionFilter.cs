using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace PCMS.API.Filters
{
    public class ExceptionFilter(ILogger<ExceptionFilter> logger, IHostEnvironment env) : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public void OnException(ExceptionContext context)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var actionName = context.ActionDescriptor.DisplayName;
            var exception = context.Exception;

            _logger.LogError(exception, "An error occurred in action {actionName} by user {userId}", actionName, userId);

            if (_env.IsDevelopment())
            {
                var details = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An error occurred while processing your request.",
                    Detail = exception.Message,
                    Instance = context.HttpContext.Request.Path
                };

                details.Extensions["StackTrace"] = exception.StackTrace;

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            else
            {
                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An error occurred while processing your request.",
                    Detail = "Please contact support if the problem persists.",
                    Instance = context.HttpContext.Request.Path
                };

                context.Result = new ObjectResult(problem)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }


}