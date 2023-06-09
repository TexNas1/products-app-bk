using ProductsApp.Application.Commands;
using ProductsApp.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsApp.Application.Products.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProduct(Guid id);
        Task<List<ProductDto>> GetProducts();  
        Task<Guid> CreateProduct(CreateProductCommand command);
        Task UpdateProduct(UpdateProductCommand command);
        Task DeleteProduct(Guid id);
    }
}
