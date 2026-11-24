using System;

namespace Demo.Solid;

public class CreateProductAppBefore
{

    /*
      Bir sınıfta değişiklik / yenilik yapmak için birden fazla sebebiniz varsa
      bu prensibi ihlal ediyorsunuz demektir.
    */
   public void CreateProduct(string name, decimal price)
    {
        /*
         db'ye bağlan.
         Insert sorgusu çalıştır.
         Dönen yanıta göre mesaj üret
        */


    }

    public void SendEmail(string to)
    {
        //mail gönderme işlemleri
    }

    public void ChangeBackColor()
    {
        //arka plan rengi değişiyor.
    }
}
public class ProductRepository
{
    public void CreateNewProduct(string name, decimal price)
    {
        System.Console.WriteLine("db ile bağlantı kuruluyor...");
        System.Console.WriteLine("Veri eklendi...");
        System.Console.WriteLine("Sonuç başarılı");
    }

    public void UpdateProduct()
    {
        
    }

    public void DeleteProduct()
    {
        
    }
}

public class EmailService
{
    public void SendEmail()
    {
        System.Console.WriteLine("Eposta gönderildi");
    }
}

public class CreateProductScreenAfter
{
    public void AddProduct()
    {
        ProductRepository productRepository = new ProductRepository();
        productRepository.CreateNewProduct("test",5);

        EmailService emailService = new EmailService();
        emailService.SendEmail();

        
    }
}
