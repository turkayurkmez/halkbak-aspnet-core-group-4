using System;
using ECommerce.Domain;

namespace ECommerce.Application.Contracts;

public interface IProductRepository : IRepository<Product>
{
   Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId);
   Task<IReadOnlyList<Product>> SearchByName(string name);
}
