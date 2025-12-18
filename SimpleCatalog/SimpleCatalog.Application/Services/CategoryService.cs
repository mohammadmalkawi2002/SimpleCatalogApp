using AutoMapper;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            category.CreatedAt = DateTime.UtcNow;

            var created = await _categoryRepository.AddAsync(category);

            return _mapper.Map<CategoryDto>(created);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id);

            if (category == null)
                throw new Exception($"Category with ID {dto.Id} not found");

           _mapper.Map(dto, category);
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<PagedResult<CategoryDto>> GetPagedCategoriesAsync(BaseQueryParametersRequest request) 
        { 
           var pagedCategories= await _categoryRepository.GetPagedAsync(request);

            var dtoItems = _mapper.Map< List<CategoryDto>>(pagedCategories.Items);

            return new PagedResult<CategoryDto> 
            { 
               Items = dtoItems,
             PageNumber=pagedCategories.PageNumber,
             PageSize=pagedCategories.PageSize,
              TotalCount=pagedCategories.TotalCount
            };
        }
    }
}
