using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

namespace Projecto.ApiCatalogo.Filter;

public class ApiLoggerFilter : IActionFilter
{
    private readonly ILogger<ApiLoggerFilter> _logger;

    public ApiLoggerFilter(ILogger<ApiLoggerFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //Executa antes 
        _logger.LogInformation("## Executando -> OnActionExecuting");
        _logger.LogInformation("###############################################");
        _logger.LogInformation($"{DateAndTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
        _logger.LogInformation("###############################################");
    }
    
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //Executa depois 
        _logger.LogInformation("## Executando -> OnActionExecuted");
        _logger.LogInformation("###############################################");
        _logger.LogInformation($"{DateAndTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"Status code : {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("###############################################");
    }
}
