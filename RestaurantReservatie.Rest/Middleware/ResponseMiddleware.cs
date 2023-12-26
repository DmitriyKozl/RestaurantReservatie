using System.Text;
using Newtonsoft.Json;

namespace RestaurantReservatie.Rest.Middleware;

public class ResponseMiddleware {
    private readonly RequestDelegate _next;

    private readonly ILogger _logger;


    public ResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) {
        _logger = loggerFactory.CreateLogger<ResponseMiddleware>();
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        finally {
            _logger.LogInformation("Request {method} {url} => {statuscode}",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode);
        }
    }
}