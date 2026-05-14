using System.Net;
using DomainLayer.Models.Common;

namespace APILayer.Middleware
{
    /// <summary>
    /// Global exception handler that sits in the middleware pipeline and catches
    /// any unhandled exception thrown by a controller or handler.
    ///
    /// Without this, an unhandled exception returns a raw 500 HTML page.
    /// With this, every error — expected or not — returns a consistent
    /// OperationResult JSON response the frontend can always rely on.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception for {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = OperationResult<string>.Failure("An unexpected error occurred.");
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
