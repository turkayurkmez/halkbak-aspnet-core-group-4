using System;

namespace ECommerce.API.Options;

public class ShopOptions
{
   /*
     "Shop":{
    "StoreName":"ECommerce",
    "MaxOrderItems": 50,
    "Currency":"EUR"
   */

   public const string SectionName ="Shop";
   public string StoreName { get; set; } = string.Empty;
   public int MaxOrderItems { get; set; }
   public string Currency { get; set; } = string.Empty;


}
