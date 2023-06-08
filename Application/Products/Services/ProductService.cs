using FluentValidation;
using ProductsApp.Application.Commands;
using ProductsApp.Application.Exceptions;
using ProductsApp.Application.Products.DTOs;
using ProductsApp.Application.Products.Services;
using ProductsApp.Domain.Entities;
using ProductsApp.Domain.Repositories;

namespace ProductsApp.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateProduct(CreateProductCommand command)
        {
            var validator = new CreateProductCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var product = new Product(Guid.NewGuid(), command.Name, command.Price, command.Description);

            await _productRepository.AddAsync(product);

            return product.Id;
        }

        public async Task UpdateProduct(UpdateProductCommand command)
        {
            var validator = new UpdateProductCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var product = await _productRepository.GetByIdAsync(command.Id) ?? throw new NotFoundException("Product not found.");
            product.Update(command.Name, command.Price, command.Description);

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id) ?? throw new NotFoundException("Product not found.");
            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null
                ? throw new NotFoundException("Product not found.")
                : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                });
            }

            return productDtos;
        }
    }
}
