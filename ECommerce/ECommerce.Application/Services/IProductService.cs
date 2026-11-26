using ECommerce.Domain;

namespace ECommerce.Application.Services;

public interface IProductService
{
    Task<Product> CreateAsync(Product product);
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetProductByIdAsync(int id);
}
