using System.Net;
using System.Text.Json;

namespace RaceDay.API.Middleware
{
    // Global exception handling middleware to catch unhandled exceptions and return uniform JSON error responses (Microsoft, 2024)
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        // Initializes a new instance of ExceptionMiddleware (Microsoft, 2024)
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Invokes the middleware pipeline step and catches unhandled exceptions (Microsoft, 2024)
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "An internal server error occurred. Please try again later.",
                detailed = exception.Message // Can be toggled based on environment settings (Microsoft, 2024)
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}