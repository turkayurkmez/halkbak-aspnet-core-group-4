using System.Diagnostics;
using ECommerce.API.Middlewares;
using ECommerce.API.Options;
using ECommerce.Application;
using ECommerce.Application.Notifications;
using ECommerce.Application.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//HttpContext nesnesinin oluştuğu yer burasıdır. Nesne içinde yer almasını istediğiniz servisler 
// ve middleware'ler builder üzerinde tanımlanmalıdır.
//Yani alet çantasını hazırladığınız yer burasıdır.
builder.Services.AddOpenApi();

builder.Services.Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SectionName));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>(); 
builder.Services.AddSingleton<INotificationChannel, EmailChannel>();
builder.Services.AddSingleton<INotificationChannel, SMSChannel>();
builder.Services.AddSingleton<INotificationChannel, WhatsAppChannel>();


builder.Services.AddScoped<NotificationService>();


var app = builder.Build();


//bu satırla birlikte çantanın fermuarını kapatmış oluyorsunuz.
//build dediğinizde tüm httpContext nesnesi içinde olması gereken servisler ve middleware'ler hazırlanır.

//Request Pipeline:
// app.Use(async(context, next) =>
// {
//    app.Logger.LogInformation("İstek geliyor....");      
//    await next(context);
//    app.Logger.LogInformation("Gelen istek: {Method} {Path} -> Dönen yanıt: {StatusCode}",context.Request.Method, context.Request.Path, context.Response.StatusCode); 
// });

// app.Use(async(context,next) =>
// {
//     var stopWatch = Stopwatch.StartNew();
//     await next();
//     stopWatch.Stop();
//     app.Logger.LogInformation("Yanıt için geçen süre: {Time} ms",stopWatch.ElapsedMilliseconds.ToString());
// });

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();

var currency = app.Configuration["Shop:Currency"];
app.Logger.LogWarning("DİKKAT!!! Currencu değeri: {currency}",currency);

app.MapGet("/ping", () => Results.Ok(new {durum="3. Aşama: Uygulama ayakta!"}));
app.MapGet("/hata", () => { throw new InvalidOperationException();});

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Run();

