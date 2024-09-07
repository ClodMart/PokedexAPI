using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace PokedexAPI.Handlers
{
    //Defines a custom middleware to handle exceptions
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

       public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);
            ProblemDetails details = new ProblemDetails() { 
                Detail = exception.Message + " " + httpContext.Request.QueryString.ToString().Replace("?", ""),
                Instance = "HttpMethod: " + httpContext.Request.Method + " Path" + httpContext.Request.Path,
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "API Server Error",
                Type = "Server Error"
            };

            string response = JsonSerializer.Serialize(details);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }
    }
}
