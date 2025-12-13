using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _categoryRepository.AddAsync(category);

            return new CategoryDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description
            };
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id);

            if (category == null)
                throw new Exception($"Category with ID {dto.Id} not found");

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
