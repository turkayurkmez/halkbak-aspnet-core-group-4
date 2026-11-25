using System;

namespace Demo.Solid;

/*
   Bir nesne ..Gelişime.. AÇIK ...Değişime.. KAPALI olmalıdır.
*/
// public enum CartType
// {
//     Standard,
//     Silver,
//     Gold,

//     Premium

// }

public abstract class CartType
{
    public abstract decimal GetDiscountedPrice(decimal total);

}

public class Standard : CartType
{
    public override decimal GetDiscountedPrice(decimal total)
    {
        return total * .95m;
    }
}

public class Silver : CartType
{
    public override decimal GetDiscountedPrice(decimal total) => total *.9m;
    
}

public class Gold : CartType
{
    public override decimal GetDiscountedPrice(decimal total) => total *.85m;
    
}

public class Premium : CartType
{
    public override decimal GetDiscountedPrice(decimal total)
    {
      return total * .8m;
    }
}

public class Customer
{
    public string Name { get; set; }
    public CartType CartType { get; set; }
}

// public class OrderProcessorBefore
// {
//     public Customer Customer { get; set; }
//     public decimal GetDiscountedPrice(decimal total)
//     {
//         switch (Customer.CartType)
//         {
//             case CartType.Standard:
//                 return total * .95m;
//             case CartType.Silver:

//                 return total * .9m;
//             case CartType.Gold:

//                 return total * .85m;

//             case CartType.Premium:
//                 return total * .8m;

//             default:
//                 return total;
//         }
//     }

// }

public class OrderProcessorAfter
{
    public Customer Customer { get; set; }
    public decimal GetDiscountedPrice(decimal total)
    {
        //burası ASLA değişmeyecek.
        return Customer.CartType.GetDiscountedPrice(total);
    }
}
