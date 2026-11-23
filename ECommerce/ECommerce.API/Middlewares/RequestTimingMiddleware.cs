using System;
using System.Diagnostics;

namespace ECommerce.API.Middlewares;

/*
 Program.cs'de her middleware'i use ile yazmak çok da akıl karı değil. Çok daha karmaşık işler yapabilecek middleware'leri 
 custom sınıflar ile daha düzgün yazabilirsiniz.
 Kurallar: sınıf adı Middleware ile biter
           next parametresi constructor'da
           context ise Invoke ya da InvokeAsync metodu içinde  (bu metot adı da gerekli)

*/
public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;   

    public RequestTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestTimingMiddleware> logger)
    {
        var stopWatch = Stopwatch.StartNew();
        logger.LogInformation("2. Kronoemetre başladı!");

        await _next(context);
        stopWatch.Stop();
        logger.LogInformation("4. Yanıt için geçen süre: {Time} ms", stopWatch.ElapsedMilliseconds.ToString());
    }



}
