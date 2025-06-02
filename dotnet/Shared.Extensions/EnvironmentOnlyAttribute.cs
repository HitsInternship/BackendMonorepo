using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class EnvironmentOnlyAttribute : ActionFilterAttribute
{
    private readonly string _environment;

    public EnvironmentOnlyAttribute(string environment)
    {
        _environment = environment;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var env = context.HttpContext.RequestServices.GetService<IHostEnvironment>();
        if (!string.Equals(env.EnvironmentName, _environment, StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new NotFoundResult();
        }
    }
}

