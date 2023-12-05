using Microsoft.Extensions.Logging;

namespace N5.Challenge.Api.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var controller = request.RouteValues["controller"]?.ToString() ?? string.Empty;
            var action = request.RouteValues["action"]?.ToString() ?? string.Empty;

            var operation = $"{controller}{action}";

            _logger.LogInformation($"Executing {operation}...");
            await _next(context);
            _logger.LogInformation($"Finished executing {operation}...");
        }
    }
}
