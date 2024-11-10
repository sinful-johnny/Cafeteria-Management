using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace api.Helpers
{


    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMiddleware> _logger;

        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request method and path
            _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

            // Log request headers including Authorization
            foreach (var header in context.Request.Headers)
            {
                _logger.LogInformation($"{header.Key}: {header.Value}");
            }

            // Log request body if present and can seek
            if (context.Request.ContentLength > 0 && context.Request.Body.CanSeek)
            {
                context.Request.Body.Position = 0; // Reset stream position
                var bodyReader = new StreamReader(context.Request.Body);
                var bodyContent = await bodyReader.ReadToEndAsync();
                _logger.LogInformation($"Request Body: {bodyContent}");
                context.Request.Body.Position = 0; // Reset stream position
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

}
