using ECommerce.Domain;

namespace ECommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly List<Product> inMemoryProducts = new()
    {
        new Product{ Id=1, Name ="Ürün A", Price=100, CategoryId=1, Sku="Test-001", Stock=100},
        new Product{ Id=2, Name ="Ürün B", Price=100, CategoryId=1, Sku="Test-002", Stock=100},
        new Product{ Id=3, Name ="Ürün C", Price=100, CategoryId=1, Sku="Test-003", Stock=100},

    };

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        await Task.CompletedTask;
        return inMemoryProducts.ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await Task.CompletedTask;
        return inMemoryProducts.FirstOrDefault(p => p.Id == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        inMemoryProducts.Add(product);
        return product;
    }


}
