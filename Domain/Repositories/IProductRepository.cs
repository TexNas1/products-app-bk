using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsApp.Domain.Entities;

namespace ProductsApp.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
