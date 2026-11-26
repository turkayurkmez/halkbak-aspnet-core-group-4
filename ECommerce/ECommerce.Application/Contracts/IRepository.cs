using System;

namespace ECommerce.Application.Contracts;

public interface IRepository<T> where T:class, new()
{
   /*
     Tüm db nesneleri (tablolar) aynı fonksiyonları içerirler.     
   */
   Task<T?> GetByIdAsync(int id);
   Task<IReadOnlyList<T>> ListAsync();

   Task AddAsync(T entity);
   void Update(T entity);
   void Remove(T entity);

   Task<bool> IsExistsAsync(int id);
}
