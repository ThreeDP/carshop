using Microsoft.AspNetCore.Mvc.Filters;

namespace CarShop.Filters;

public class CarShopLoggingFilter : IActionFilter {
    private readonly ILogger<CarShopLoggingFilter> _logger;

    public CarShopLoggingFilter(ILogger<CarShopLoggingFilter> logger) {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext ctx)
    {
        _logger.LogInformation("==== OnActionExecuting ====");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState: {ctx.ModelState.IsValid}");
    }

    public void OnActionExecuted(ActionExecutedContext ctx) {
        _logger.LogInformation("==== OnActionExecuted ====");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState: {ctx.HttpContext.Response.StatusCode}");
    }
}