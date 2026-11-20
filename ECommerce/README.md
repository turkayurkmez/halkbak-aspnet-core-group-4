# E-Commerce Eğitim Projesi

Bu repo, ASP.NET Core Web API konularını uygulamalı olarak öğrenmek için kullanılan bir eğitim projesidir. Günlük ders notları aşağıda tarihe göre tutulur.

## Günlük Ders Notları

### 20 Kasım 2026

- **ASP.NET Core uygulama başlangıcı:** `WebApplication.CreateBuilder` ile uygulama yapılandırmasını hazırlama ve `builder.Build()` ile uygulamayı oluşturma.
- **HTTP istek pipeline'ı:** Middleware'leri `UseMiddleware` ile sıraya ekleme ve her middleware'den sonraki adıma `RequestDelegate` (`_next`) aracılığıyla geçme.
- **Özel middleware yazımı:** Constructor'da `RequestDelegate` alma, `InvokeAsync` içinde `HttpContext` ile çalıştırma.
- **İstek loglama:** İstek öncesinde ve endpoint çalıştıktan sonra HTTP metodu, yolu ve yanıt durum kodunu loglama (`RequestLoggingMiddleware`).
- **İşlem süresini ölçme:** `Stopwatch` ile isteğin işlenme süresini ölçüp loglama (`RequestTimingMiddleware`).
- **Minimal API endpoint'i:** `/ping` adresinde uygulamanın ayakta olduğunu bildiren bir `GET` endpoint'i tanımlama.

Bugünkü örnekte pipeline sırası önce istek loglama, sonra süre ölçümüdür; her iki middleware de endpoint'e geçişi `_next(context)` ile sağlar.

## Proje Yapısı

- `ECommerce.API/Program.cs`: Uygulama başlangıcı, middleware sırası ve `/ping` endpoint'i.
- `ECommerce.API/Middlewares/`: İstek loglama ve süre ölçümü middleware sınıfları.

## Günlük Güncelleme

Her ders gününde `Günlük Ders Notları` bölümünün en üstüne yeni bir tarih başlığı ekleyip o gün işlenen konuları ve ilgili örnekleri maddeler halinde yazın.