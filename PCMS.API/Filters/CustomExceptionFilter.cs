using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PCMS.API.Filters
{
    public class CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IWebHostEnvironment env) : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger = logger;
        private readonly IWebHostEnvironment _env = env;

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception has occurred.");

            var error = new
            {
                error = "An error occurred while processing your request.",
                details = _env.IsDevelopment() ? context.Exception.Message : "See application log for details"
            };

            var result = new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}