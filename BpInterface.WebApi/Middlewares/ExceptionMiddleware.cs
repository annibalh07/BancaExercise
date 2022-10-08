using BpInterface.Core.Exceptions;
using System.Net.Mime;
using System.Text.Json;

namespace BpInterface.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Method invoked when a error is raised
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string response;

            if (exception is HttpResponseException httpResponseException)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = httpResponseException.Status;

                response = JsonSerializer.Serialize(new
                {
                    Text = httpResponseException.Msg
                });
                _logger.LogInformation("HttpResponseException {Status} {Response}",
                    httpResponseException.Status, response);
            }
            else
            {
                response = JsonSerializer.Serialize(new { error = "Internal Server Error" });
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                _logger.LogError(exception, exception.Message);
            }
            return context.Response.WriteAsync(response);
        }
    }
}
