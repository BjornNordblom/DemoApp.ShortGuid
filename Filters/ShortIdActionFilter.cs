using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

//https://stackoverflow.com/questions/57153772/conditional-validation-based-on-request-route-with-asp-net-core-2-2-and-fluentva

public class ShortIdValidationAttribute : IActionFilter
{
    private readonly IShortIdFactory _factory;

    public ShortIdValidationAttribute(IShortIdFactory factory)
    {
        _factory = factory;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.ActionArguments.TryGetValue("id", out var id);
        if (id is null)
            return;
        var strId = id.ToString();
        if (strId is null)
            return;
        var isValid = _factory.Validate(strId);
        _factory.TryParse(strId, out var shortId);
        if (shortId == null)
        {
            context.Result = new BadRequestObjectResult("Invalid Id");
            return;
        }
        context.ActionArguments["id"] = shortId;
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
