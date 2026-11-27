using System;
using ECommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Customer> Customers  => Set<Customer>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Product>().Property(p=>p.Name).IsRequired()
                                                         .HasMaxLength(200);

       modelBuilder.Entity<Product>().Property(p=>p.Sku).IsRequired().HasMaxLength(50);
       modelBuilder.Entity<Product>().Property(p=>p.Price).HasColumnType("decimal(18,2)");

       modelBuilder.Entity<Product>().HasIndex(p=>p.Sku).IsUnique();

       //Eğer Product ile Category arasında property'ler varsa EF Core bunu otomatik olarak algılar.
       //Yoksa aşağıdaki gibi yazmanız gerekir.
       modelBuilder.Entity<Product>().HasOne(p=>p.Category)
                                     .WithMany(c=>c.Products)
                                     .HasForeignKey(p=>p.CategoryId)
                                     .OnDelete(DeleteBehavior.Restrict);



       modelBuilder.Entity<Order>().HasMany(o=>o.Items)
                                   .WithOne()
                                   .HasForeignKey(oi=>oi.OrderId)
                                   .OnDelete(DeleteBehavior.Cascade);


       modelBuilder.Entity<OrderItem>().HasOne(oi=>oi.Product)
                                       .WithMany()
                                       .HasForeignKey(oi=>oi.ProductId)
                                       .OnDelete(DeleteBehavior.Restrict);

    }

    




}
