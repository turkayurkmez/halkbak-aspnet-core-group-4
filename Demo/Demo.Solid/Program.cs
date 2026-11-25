// See https://aka.ms/new-console-template for more information
using Demo.Solid;

Console.WriteLine("Hello, World!");

/*
  Single Responsibility
  Open/Closes
  Liskov's Substution
  Interface Segregation
  Dependency Inversion

*/

Customer customer = new Customer { CartType = new Premium(), Name ="Türkay"};
//OrderProcessorBefore orderProcessorBefore = new OrderProcessorBefore{ Customer = customer};
OrderProcessorAfter orderProcessorAfter = new OrderProcessorAfter { Customer = customer};

System.Console.WriteLine(orderProcessorAfter.GetDiscountedPrice(1000));