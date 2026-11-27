# E-Commerce Eğitim Projesi

Bu repo, ASP.NET Core Web API konularını uygulamalı olarak öğrenmek için kullanılan bir eğitim projesidir. Günlük ders notları aşağıda tarihe göre tutulur.

## Günlük Ders Notları

### 27 Kasım 2026

- **Infrastructure katmanı:** Çözüme `ECommerce.Infrastructure` sınıf kütüphanesi eklendi; veri erişimi için `Microsoft.EntityFrameworkCore.Sqlite` paketini kullanır. Proje başta `ECommerce.Domain`'e referans veriyordu, sonra `ECommerce.Application`'a referans verecek şekilde değiştirildi (Domain'e dolaylı erişir). `ECommerce.API` artık doğrudan `ECommerce.Infrastructure`'a referans verir; Application ve Domain'e bu zincir üzerinden ulaşır.
- **EF Core araçları:** `ECommerce.Infrastructure/dotnet-tools.json` ile `dotnet-ef` (10.0.15) yerel araç olarak tanımlanır; `ECommerce.API` projesine de `Microsoft.EntityFrameworkCore.Design` paketi eklendi. Bu paket startup projesinde (API) bulunmalıdır.
- **Domain modelleri:** `Customer` (ad, e-posta, siparişler), `Order` (müşteri, oluşturulma tarihi, kalemler, `CalculateTotal()` ile toplam tutar) ve `OrderItem` (ürün, adet, birim fiyat) eklendi; `Category` ürün koleksiyonu kazandı, `Product`'a doğrulama öznitelikleri (`Required`, `MinLength(2)`, `MaxLength(200)`) eklendi.
- **AppDbContext:** `Data/AppDbContext.cs` içinde `Products`, `Categories`, `Orders`, `Customers` ve `OrderItems` `DbSet`'leri bulunur. `OnModelCreating` ile `Product.Name` zorunlu ve en çok 200 karakter, `Sku` zorunlu, en çok 50 karakter ve tekil (unique index), `Price` `decimal(18,2)` olur. `Product`→`Category` ve `OrderItem`→`Product` ilişkilerinde silme davranışı `Restrict`, `Order`→`Items` ilişkisinde `Cascade`'dir.
- **Bağlantı ve kayıt:** `appsettings.json` içine `ConnectionStrings:Default` (`Data Source=ecommerce.db`) eklendi; `Program.cs` içinde `AddDbContext<AppDbContext>` ile SQLite kullanılacak şekilde kaydedilir.
- **Migration:** `Data/Migrations` altında `InitialCreate` migration'ı ve model snapshot'ı oluşturuldu (Categories, Customers, Products, Orders, OrderItems tabloları). Oluşan `ecommerce.db` dosyası da repoya eklenmiştir.
- **Örneğin sınırları:** Servisler hâlâ bellek içi listeleri kullanır; `IRepository<T>`/`IProductRepository` uygulamaları yoktur ve `Class1` hâlâ durur. `DbContext` yalnızca kayıtlıdır, controller/servisler tarafından kullanılmaz.

Migration komutları (`ECommerce.Infrastructure` dizininden; önce `dotnet tool restore`):

```bash
dotnet ef migrations add InitialCreate -s ../ECommerce.API/ECommerce.API.csproj -o Data/Migrations
dotnet ef database update -s ../ECommerce.API/ECommerce.API.csproj
```

`-s` değeri `.csproj` dosyasını göstermelidir; yanlış yol "Unable to retrieve project metadata" uyarısına yol açabilir.

### 26 Kasım 2026

- **Repository sözleşmeleri:** `ECommerce.Application/Contracts` altına genel `IRepository<T>` (`GetByIdAsync`, `ListAsync`, `AddAsync`, `Update`, `Remove`, `IsExistsAsync`) ve ürüne özgü `IProductRepository` (`GetByCategoryAsync`, `SearchByName`) arayüzleri eklendi. Henüz uygulaması yoktur; Infrastructure katmanında uygulanması planlanır.
- **Bağımlılık enjeksiyonu ömürleri (lifetime):** `Demo/Demo.LifeTime`, ASP.NET Core MVC şablonu üzerine kurulu bir demodur. `Services/LifeTimeService.cs` içinde `Guid` değerini tutan ortak `IGuidGenerator` arayüzünden türeyen `ISingleton`, `IScoped` ve `ITransient` arayüzleri ile bunların `Singleton`, `Scoped` ve `Transient` uygulamaları bulunur. Her uygulama, oluşturulurken (constructor) yeni bir `Guid` üretir; böylece aynı nesnenin mi yoksa yeni bir nesnenin mi kullanıldığı `Guid` değerinden anlaşılır.
- **Kayıtlar:** `Program.cs` içinde `ISingleton` → `AddSingleton`, `IScoped` → `AddScoped`, `ITransient` → `AddTransient` ile kaydedilir. `LifeTimeService` de transient olarak kaydedilir ve constructor'ında üç servisi alıp `Singleton`, `Scoped`, `Transient` özelliklerinde tutar.
- **Gözlem:** `HomeController`, üç servisi doğrudan, `LifeTimeService` üzerinden de bir kez daha alır. `Index` action'ı bu altı `Guid` değerini `ViewBag` ile `Views/Home/Index.cshtml` sayfasına aktarır; sayfa önce doğrudan enjekte edilen, ardından `LifeTimeService` içindeki değerleri listeler.
- **Beklenen sonuç:** Singleton değeri uygulama çalıştığı sürece tüm isteklerde ve iki satırda aynıdır. Scoped değeri aynı istekte iki satırda aynıdır, her yeni istekte (sayfa yenilemede) değişir. Transient değeri ise aynı istek içinde bile iki satırda farklıdır; çünkü her bağımlılık çözümlemesinde yeni nesne oluşturulur.
- **Arayüz tabanlı kayıt (ECommerce):** `ECommerce.Application/Services` altına `IOrderService` ve `IProductService` arayüzleri eklendi. `OrderService` ve `ProductService` bu arayüzleri uygular; `Program.cs` içinde `AddScoped<IProductService, ProductService>` ve `AddScoped<IOrderService, OrderService>` olarak kaydedilir. `OrdersController` ve `ProductsController` artık somut sınıflar yerine arayüzlere bağımlıdır. `ProductService`'in namespace'i `ECommerce.Application` yerine `ECommerce.Application.Services` oldu; endpoint davranışları değişmedi.
- **Çözüm dosyası:** `Demo.LifeTime` projesi `ECommerce.slnx` çözümüne eklendi.
- **Örneğin sınırları:** Demo yalnızca ömür farklarını gösterir; Singleton, Scoped ve Transient sınıfları iş mantığı içermez. `ViewBag` kullanımı örneği kısa tutmak içindir.

`ECommerce` dizininden örnek çalıştırma:

```bash
dotnet run --project ../Demo/Demo.LifeTime
```

Açılan sayfada tarayıcıyı birkaç kez yenileyip hangi `Guid` değerlerinin değiştiğini karşılaştırın.

### 25 Kasım 2026

- **Sipariş ve bildirim akışı:** `POST /api/orders`, `OrdersController` üzerinden `OrderService.CreateOrder` metodunu çağırır ve işlem tamamlandığında HTTP 200 döndürür. Servis, `NotificationService` aracılığıyla örnek müşteriye “Siparişiniz oluşturuldu” mesajını iletir.
- **ECommerce'de SRP:** Sipariş işlemini `OrderService`, bildirimlerin dağıtımını `NotificationService`, kanala özgü gönderim davranışını ise `EmailChannel`, `SMSChannel` ve `WhatsAppChannel` üstlenir. Böylece sipariş akışı ile bildirim sorumlulukları ayrılır.
- **ECommerce'de OCP:** Kanallar ortak `INotificationChannel` arayüzündeki `Name` ve `SendAsync` üyelerini uygular. `NotificationService`, bağımlılık enjeksiyonundan aldığı `IEnumerable<INotificationChannel>` içindeki kanalları sırayla çağırır. Yeni bir kanal eklemek için arayüzü uygulayan bir sınıf yazıp `Program.cs` içinde `INotificationChannel` olarak kaydetmek yeterlidir; bildirim servisini değiştirmek gerekmez. Mevcut kanallar singleton, `NotificationService` ve `OrderService` ise scoped olarak kayıtlıdır.
- **Örneğin sınırları:** Sipariş veritabanına kaydedilmez; müşteri adresi ve mesajı örnek değerlerdir. Üç kanal gerçek e-posta, SMS veya WhatsApp mesajı göndermek yerine yalnızca bilgi seviyesinde log yazar. Aynı alıcı ve mesaj kayıtlı tüm kanallara iletilir.
- **OCP demosu:** `Demo/Demo.Solid/OCP.cs`, indirimleri `switch` ile seçen eski yaklaşım yerine `CartType` soyut sınıfını ve `Standard`, `Silver`, `Gold`, `Premium` uygulamalarını kullanır. `OrderProcessorAfter`, indirim hesabını müşterinin kart tipine devreder; yeni bir kart tipi işlemci değiştirilmeden eklenebilir.
- **LSP demosu:** `Demo/Demo.Solid/LSP.cs` içinde `Rectangle` ve `Square`, birbirinden türemek yerine ortak `IArea` arayüzünü uygular. `Geometry.CreateRect`, verilen ölçülere göre kare veya dikdörtgen döndürür; çağıran kod her ikisinin alanını aynı arayüz üzerinden hesaplar.
- **Konsol çıktısı:** SOLID demosu artık `Hello, World!` yanında Premium kart için 1000 üzerinden indirimli fiyatı (800) ve örnek karenin ve dikdörtgenin alanlarını (25 ve 15) da yazdırır.

### 24 Kasım 2026

- **Katmanlı yapı:** `ECommerce.Application` ve `ECommerce.Domain` projeleri eklendi. API, Application'a; Application da Domain'e referans verir. Domain'deki `Product` ürünün kimlik, ad, SKU, fiyat, stok ve kategori kimliği alanlarını; `Category` ise kimlik ve ad alanlarını içerir.
- **Ürün servisi ve bağımlılık enjeksiyonu:** `ProductService`, `AddScoped` ile kaydedilir ve `ProductsController` tarafından kullanılır. Servis, başlangıçta üç ürün içeren örnek bir bellek içi liste üzerinden ürünleri listeler, kimliğe göre arar ve yeni ürün ekler.
- **Ürün endpoint'leri:** `GET /api/products` ürünleri listeler; `GET /api/products/{id}` kimliğe göre arar; `POST /api/products/add` istek gövdesindeki ürünü ekler ve `CreatedAtAction` ile ürünün adresini içeren yanıt döndürür. `GET /api/products/info` mağaza ayarlarını döndürmeye devam eder.
- **Örneğin sınırları:** Ürün listesi veritabanına kaydedilmez. Servis her HTTP isteği için ayrı oluşturulduğundan eklenen ürünler sonraki isteklerde korunmaz. Yeni ürünün kimliği servis tarafından üretilmez; bulunamayan ürün için ayrıca `NotFound` yanıtı tanımlanmamıştır.
- **OpenAPI ve Scalar:** Development ortamında OpenAPI ve Scalar etkinleştirilir. `http` profili tarayıcıyı `/scalar` adresinde açacak şekilde ayarlanmıştır; Production ortamında bu endpoint'ler eşlenmez.
- **SOLID ve tek sorumluluk ilkesi (SRP):** `Demo/Demo.Solid` konsol projesinde SOLID ilkeleri listelenir. `SRP.cs`, ürün oluşturma, e-posta ve arayüz sorumluluklarını tek sınıfta toplama örneğini; ardından ürün deposu, e-posta servisi ve ekran sınıflarına ayıran bir örneği gösterir. Konsolun güncel davranışı için 25 Kasım notlarına bakın.

### 23 Kasım 2026

- **Merkezi hata yönetimi:** `ExceptionHandlingMiddleware`, sonraki aşamalarda oluşan hataları loglar. Yanıt henüz başlamamışsa HTTP 500 ve `{"error":"Beklenmeyen bir hata oluştu"}` döndürür; başlamışsa hatayı yeniden fırlatır. `/hata` endpoint'i bu davranışı denemek için istisna üretir.
- **Middleware sırası:** Pipeline'da önce hata yönetimi, ardından istek loglama ve süre ölçümü bulunur. Normal akışta istek logu ve kronometre endpoint'ten önce, süre ve yanıt logları ise endpoint'ten sonra çalışır.
- **Controller tabanlı API:** `AddControllers` ve `MapControllers` ile controller'lar etkinleştirilir. O gün `GET /api/products` örnek bir mesaj, `GET /api/products/info` ise mağaza ayarlarını döndürüyordu; ürün endpoint'lerinin güncel davranışı için 24 Kasım notlarına bakın.
- **Options ve yapılandırma:** `Shop` bölümü `ShopOptions` sınıfına bağlanır ve controller'a `IOptions<ShopOptions>` ile aktarılır. `StoreName`, `MaxOrderItems` ve `Currency` ayarları tanımlıdır; `Program.cs` içinde `Shop:Currency` değerine doğrudan yapılandırma üzerinden de erişilir.
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
- `ECommerce.API/Controllers/`: Ürün, mağaza bilgisi ve sipariş endpoint'leri.
- `ECommerce.API/Options/`: Mağaza yapılandırmasını temsil eden `ShopOptions` sınıfı.
- `ECommerce.API/appsettings*.json`: Temel ve ortama özgü yapılandırma.
- `ECommerce.API/Properties/launchSettings.json`: Geliştirme ve üretim başlatma profilleri.
- `ECommerce.Application/Services/`: `IProductService`/`IOrderService` arayüzleri; bellek içi ürün işlemleri ve örnek sipariş servisi.
- `ECommerce.Application/Notifications/`: Bildirim kanalı arayüzü, kanal uygulamaları ve bildirim servisi.
- `ECommerce.Application/Contracts/`: Repository arayüzleri (`IRepository<T>`, `IProductRepository`).
- `ECommerce.Domain/`: Ürün, kategori, müşteri, sipariş ve sipariş kalemi modelleri.
- `ECommerce.Infrastructure/`: EF Core (SQLite) `AppDbContext`'i, `Data/Migrations` ve `dotnet-ef` araç tanımı.
- `../Demo/Demo.Solid/`: SRP, OCP ve LSP örneklerini içeren konsol projesi.
- `../Demo/Demo.LifeTime/`: Singleton, Scoped ve Transient ömürlerini gösteren ASP.NET Core MVC projesi.
- `ECommerce.slnx`: API, Application, Domain, Infrastructure, SOLID ve LifeTime demo projelerini içeren çözüm.

## Günlük Güncelleme

Her ders gününde `Günlük Ders Notları` bölümünün en üstüne yeni bir tarih başlığı ekleyip o gün işlenen konuları ve ilgili örnekleri maddeler halinde yazın.