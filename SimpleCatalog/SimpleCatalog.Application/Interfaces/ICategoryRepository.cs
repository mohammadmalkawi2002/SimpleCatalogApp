using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
