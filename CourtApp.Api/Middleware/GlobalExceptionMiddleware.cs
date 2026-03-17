using CourtApp.Application.Common;
using System.Net;
using System.Text.Json;

namespace CourtApp.Api.Middleware
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "[{CorrelationId}] Unhandled exception — {Method} {Path}",
                    context.TraceIdentifier,
                    context.Request.Method,
                    context.Request.Path);

                await WriteErrorResponseAsync(context);
            }
        }

        private static async Task WriteErrorResponseAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = ApiResponse<object>.ServerError(
                "An unexpected error occurred. Please try again later.");

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, _jsonOptions));
        }
    }
}
