using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }


        public async Task<List<ProductDto>> GetAllProductsAsync() 
        { 
            var products=  await _productRepo.GetAllAsync();

            return products.Select(p => new ProductDto 
                {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Status = p.Status,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            }).ToList();
        }


        public async Task<ProductDto> GetProductByIdAsync(int id) 
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product is null)
                return null;

            return new ProductDto 
            { 
             Id= product.Id,
             Name = product.Name,
             Description = product.Description,
             Price = product.Price,
             Status = product.Status,
             CategoryId = product.CategoryId,
             CategoryName=product.Category?.Name ?? string.Empty
            
            };
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto) 
        {
            //Mapping From Dto To Entity:
            var product = new Product 
            { 
             Name=dto.Name,
             Description=dto.Description,
             Price=dto.Price,
             Status = dto.Status,
             CategoryId = dto.CategoryId,
              CreatedAt = DateTime.UtcNow

            };

            var created = await _productRepo.AddAsync(product);

            return new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                Status = created.Status,
                CategoryId = created.CategoryId
            };
        }

        public async Task UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.Id);

            if (product == null)
                throw new Exception($"Product with ID {dto.Id} not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Status = dto.Status;
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _productRepo.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepo.DeleteAsync(id);
        }


    }
}
