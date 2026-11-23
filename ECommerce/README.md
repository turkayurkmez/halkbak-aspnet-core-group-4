# E-Commerce Eğitim Projesi

Bu repo, ASP.NET Core Web API konularını uygulamalı olarak öğrenmek için kullanılan bir eğitim projesidir. Günlük ders notları aşağıda tarihe göre tutulur.

## Günlük Ders Notları

### 23 Kasım 2026

- **Merkezi hata yönetimi:** `ExceptionHandlingMiddleware`, sonraki aşamalarda oluşan hataları loglar. Yanıt henüz başlamamışsa HTTP 500 ve `{"error":"Beklenmeyen bir hata oluştu"}` döndürür; başlamışsa hatayı yeniden fırlatır. `/hata` endpoint'i bu davranışı denemek için istisna üretir.
- **Middleware sırası:** Pipeline'da önce hata yönetimi, ardından istek loglama ve süre ölçümü bulunur. Normal akışta istek logu ve kronometre endpoint'ten önce, süre ve yanıt logları ise endpoint'ten sonra çalışır.
- **Controller tabanlı API:** `AddControllers` ve `MapControllers` ile controller'lar etkinleştirilir. `GET /api/products` şimdilik örnek bir mesaj, `GET /api/products/info` ise mağaza ayarlarını döndürür.
- **Options ve yapılandırma:** `Shop` bölümü `ShopOptions` sınıfına bağlanır ve controller'a `IOptions<ShopOptions>` ile aktarılır. `StoreName`, `MaxOrderItems` ve `Currency` ayarları tanımlıdır; `Shop:Currency` değerine doğrudan yapılandırma üzerinden de erişilir.
- **Ortamlar:** Temel yapılandırmada para birimi `USD`, Development ortamında `TL`, Production ortamında `EUR` olarak tanımlıdır. Ortama özgü ayarlar temel ayarları geçersiz kılar; diğer yapılandırma kaynakları da etkin değeri değiştirebilir. Projede User Secrets kimliği de tanımlanmıştır.
- **Başlatma profilleri:** `http` profili Development ortamında `http://localhost:5050`, `http-pro` profili Production ortamında `http://localhost:6060` adresini kullanır. `https` profili Development ortamında `https://localhost:7255` ve `http://localhost:5031` adreslerini kullanır.
- **Geliştirme konteyneri:** .NET 10 geliştirme konteyneri, VS Code uzantıları ve çözüm için `dotnet restore` adımı eklenmiştir. Konteyner yapılandırması 5000, 5031 ve 7255 portlarını yönlendirir; 5050 ve 6060 profilleri için gerekirse bu portları ayrıca yönlendirin.

`ECommerce` dizininden örnek çalıştırma:

```bash
dotnet run --project ECommerce.API --launch-profile http
dotnet run --project ECommerce.API --launch-profile http-pro
```

### 20 Kasım 2026

- **ASP.NET Core uygulama başlangıcı:** `WebApplication.CreateBuilder` ile uygulama yapılandırmasını hazırlama ve `builder.Build()` ile uygulamayı oluşturma.
- **HTTP istek pipeline'ı:** Middleware'leri `UseMiddleware` ile sıraya ekleme ve her middleware'den sonraki adıma `RequestDelegate` (`_next`) aracılığıyla geçme.
- **Özel middleware yazımı:** Constructor'da `RequestDelegate` alma, `InvokeAsync` içinde `HttpContext` ile çalıştırma.
- **İstek loglama:** İstek öncesinde ve endpoint çalıştıktan sonra HTTP metodu, yolu ve yanıt durum kodunu loglama (`RequestLoggingMiddleware`).
- **İşlem süresini ölçme:** `Stopwatch` ile isteğin işlenme süresini ölçüp loglama (`RequestTimingMiddleware`).
- **Minimal API endpoint'i:** `/ping` adresinde uygulamanın ayakta olduğunu bildiren bir `GET` endpoint'i tanımlama.

Bugünkü örnekte pipeline sırası önce istek loglama, sonra süre ölçümüdür; her iki middleware de endpoint'e geçişi `_next(context)` ile sağlar.

## Proje Yapısı

- `ECommerce.API/Program.cs`: Uygulama başlangıcı, servisler, middleware sırası ve minimal API endpoint'leri.
- `ECommerce.API/Middlewares/`: Hata yönetimi, istek loglama ve süre ölçümü middleware sınıfları.
- `ECommerce.API/Controllers/`: Ürün ve mağaza bilgisi endpoint'leri.
- `ECommerce.API/Options/`: Mağaza yapılandırmasını temsil eden `ShopOptions` sınıfı.
- `ECommerce.API/appsettings*.json`: Temel ve ortama özgü yapılandırma.
- `ECommerce.API/Properties/launchSettings.json`: Geliştirme ve üretim başlatma profilleri.

## Günlük Güncelleme

Her ders gününde `Günlük Ders Notları` bölümünün en üstüne yeni bir tarih başlığı ekleyip o gün işlenen konuları ve ilgili örnekleri maddeler halinde yazın.