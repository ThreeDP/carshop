using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarShop.Filters;

public class ApiExceptionFilter : IExceptionFilter {
    private readonly ILogger<ApiExceptionFilter> _looger;
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) {
        _looger = logger;
    }

    public void OnException(ExceptionContext ctx) {
        _looger.LogError(ctx.Exception, "Ocorreu um erro inesperado ou não tratado.");
        ctx.Result = new ObjectResult("Houve um problema ao tratar sua solicitação.") {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }

}