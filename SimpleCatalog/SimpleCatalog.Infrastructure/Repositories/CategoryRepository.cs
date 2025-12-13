using Microsoft.EntityFrameworkCore;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Domain.Entities;
using SimpleCatalog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(int id)
        {
          var category=await _context.Categories.FindAsync(id);
            if (category != null) 
            { 
             _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

      
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
           
        }
    }
}
