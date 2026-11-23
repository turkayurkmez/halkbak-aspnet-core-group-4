using System;

namespace ECommerce.API.Middlewares;

public class RequestLoggingMiddleware
{

    private RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestLoggingMiddleware> logger)
    {
        logger.LogInformation("1. İstek geliyor....");    

        await _next(context);
        logger.LogInformation("5. Gelen istek: {Method} {Path} -> Dönen yanıt: {StatusCode}", context.Request.Method, context.Request.Path, context.Response.StatusCode);
    }



}
