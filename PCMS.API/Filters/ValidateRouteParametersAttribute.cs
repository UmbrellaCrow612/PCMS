using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace PCMS.API.Filters
{
    /// <summary>
    /// Custom Filter to make sure route params are not null, empty or white space
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateRouteParametersAttribute(ILogger<ValidateRouteParametersAttribute> logger) : ActionFilterAttribute
    {
        private readonly ILogger<ValidateRouteParametersAttribute> _logger = logger;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Validating route parameters for {Controller}.{Action}",
                context.ActionDescriptor.RouteValues["controller"],
                context.ActionDescriptor.RouteValues["action"]);

            foreach (var kvp in context.RouteData.Values)
            {
                if (kvp.Value is string stringValue)
                {
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        _logger.LogWarning("Invalid route parameter: {Key} is null, empty, or whitespace", kvp.Key);
                        context.Result = new BadRequestObjectResult($"Route parameter '{kvp.Key}' cannot be null, empty, or whitespace.");
                        return;
                    }
                    else
                    {
                        _logger.LogDebug("Valid route parameter: {Key} = {Value}", kvp.Key, stringValue);
                    }
                }
            }

            _logger.LogInformation("All route parameters valid for {Controller}.{Action}",
                context.ActionDescriptor.RouteValues["controller"],
                context.ActionDescriptor.RouteValues["action"]);

            base.OnActionExecuting(context);
        }
    }
}