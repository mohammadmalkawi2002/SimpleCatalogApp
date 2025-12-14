using AutoMapper;
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
        private readonly IMapper _mapper;
        

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        public async Task<List<ProductDto>> GetAllProductsAsync() 
        { 
            var products=  await _productRepo.GetAllAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }


        public async Task<ProductDto> GetProductByIdAsync(int id) 
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product is null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto) 
        {
            //Mapping From Dto To Entity:
            var product = _mapper.Map<Product>(dto);
            product.CreatedAt = DateTime.UtcNow;
            var created = await _productRepo.AddAsync(product);

            return _mapper.Map<ProductDto>(created);
        }

        public async Task UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.Id);


            if (product == null)
                throw new Exception($"Product with ID {dto.Id} not found");

            product.UpdatedAt = DateTime.UtcNow;

            _mapper.Map(dto,product);

            await _productRepo.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepo.DeleteAsync(id);
        }


    }
}
