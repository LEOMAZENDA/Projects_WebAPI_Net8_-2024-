using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Projecto.ApiCatalogo.Filter;

public class ApiExcepionFilter : IExceptionFilter
{

    private readonly ILogger<IExceptionFilter> _logger;

    public ApiExcepionFilter(ILogger<IExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu uma exceção não tratada: Status code 500");

        context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação: Status code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}