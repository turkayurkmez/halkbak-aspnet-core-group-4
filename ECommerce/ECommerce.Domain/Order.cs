using System;

namespace ECommerce.Domain;

public class Order
{
   public int Id { get; set; }
   public int CustomerId { get; set; }
   public DateTime CreatedAt { get; set; }

   public virtual Customer? Customer { get; set; }

   // TODO 1: Bu siparişte hangi üründen kaç adet satın aldığını gösteren koleksiyon
   public List<OrderItem> Items { get; set; } = [];
 
   // TODO 2: O koleksiyon ile hesaplanacak Toplam Fiyat:
   public decimal CalculateTotal() => Items.Sum(x=>x.UnitPrice * x.Quantity);

   
}
