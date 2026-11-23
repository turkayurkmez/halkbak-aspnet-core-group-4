using System;

namespace ECommerce.API.Middlewares;

public class ExceptionHandlingMiddleware
{
   private readonly RequestDelegate _next;
   private readonly ILogger<ExceptionHandlingMiddleware> _logger;
   public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger )
   {
      _next = next;
      _logger = logger;
   }

   public async Task InvokeAsync(HttpContext context)
    {
        //http isteği herhangi bir aşamada exception ile karşılaşırsa ne yapmak istiyoruz?
        try
        {
            await _next(context);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Bilinmeyen bir hata meydana geldi: {Method},{Path}", context.Request.Method, context.Request.Path);

            if (context.Response.HasStarted)
            {
                throw;
            }
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new {error = "Beklenmeyen bir hata oluştu"});
        }
        //Bir middlleware'in life-cycle'ı (nesne ömrü), singleton'dur. 
        //Yani constructor, sadece 1 kez çalışır. Bu durumda birden fazla çalışması gereken bir objeye bağımlılığı olduğunda (Mesela Db objesi) o zaman
        // bu bağımlılık Metot parametresi olarak girilmeli.



    }
}
