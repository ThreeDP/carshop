using Microsoft.AspNetCore.Mvc.Filters;

namespace CarShop.Filters;

public class CarShopLoggingFilter : IActionFilter {
    private readonly ILogger<CarShopLoggingFilter> _logger;

    public CarShopLoggingFilter(ILogger<CarShopLoggingFilter> logger) {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext ctx) {
        _logger.LogInformation($"Request on {ctx.RouteData.Values["controller"]} with method {ctx.RouteData.Values["action"]} [ {DateTime.Now.ToLongTimeString()} ] - model is valid: {ctx.ModelState.IsValid}");
    }

    public void OnActionExecuted(ActionExecutedContext ctx) {
        _logger.LogInformation($"Response from {ctx.RouteData.Values["controller"]}/{ctx.RouteData.Values["action"]} [ {DateTime.Now.ToLongTimeString()} ] - status: {ctx.HttpContext.Response.StatusCode}");
    }
}