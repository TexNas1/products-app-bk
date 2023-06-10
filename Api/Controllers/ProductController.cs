using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductsApp.Application.Commands;
using ProductsApp.Application.Exceptions;
using ProductsApp.Application.Products.DTOs;
using ProductsApp.Application.Products.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsApp.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            try
            {
                var product = await _productService.GetProduct(id);
                return Ok(product);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct(CreateProductCommand command)
        {
            try
            {
                var productId = await _productService.CreateProduct(command);
                return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            try
            {
                command.Id = id;
                await _productService.UpdateProduct(command);
                return Ok(new { message = "Product updated successfully" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
