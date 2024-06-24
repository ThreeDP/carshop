using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CarShop.Filters;

public class CarShopLoggingFilter : IActionFilter {
    private readonly ILogger<CarShopLoggingFilter> _logger;

    public CarShopLoggingFilter(ILogger<CarShopLoggingFilter> logger) {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext ctx) {
        var modelValid = ctx.ModelState.IsValid ? "modelo válido" : "erro no modelo"; 
        _logger.LogInformation($"Request on {ctx.RouteData.Values["controller"]} with method {ctx.RouteData.Values["action"]} [ {DateTime.Now.ToLongTimeString()} ] - {modelValid}");
    }

    public void OnActionExecuted(ActionExecutedContext ctx) {
        string log = $"\tResponse from [ {ctx.RouteData.Values["action"]}/{ctx.RouteData.Values["controller"]} ]\n";
        log += $"\tDateTime: [ {DateTime.Now.ToLongTimeString()} ]\n";
        log += $"\tStatus Code: [ {ctx.HttpContext.Response.StatusCode} ]";
        _logger.LogInformation(log);
    }
}