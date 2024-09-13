using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PCMS.API.Filters
{
    /// <summary>
    /// Custom Filter to make sure route params are not null, empty or white space
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateRouteParametersAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var kvp in context.ActionArguments)
            {
                if (kvp.Value is string stringValue)
                {
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        context.Result = new BadRequestObjectResult($"Route parameter '{kvp.Key}' cannot be null, empty, or whitespace.");
                        return;
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
