using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PCMS.API.Filters
{
    public class CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger = logger;

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception has occurred.");

            var result = new ObjectResult(new
            {
                error = "An error occurred while processing your request.",
                details = context.Exception.Message
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}