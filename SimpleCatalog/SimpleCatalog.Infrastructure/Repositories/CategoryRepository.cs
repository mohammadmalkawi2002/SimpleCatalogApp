using Microsoft.EntityFrameworkCore;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Application.Pagination;
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

        public async Task<PagedResult<Category>> GetPagedAsync(BaseQueryParametersRequest query)
        {
           var queryable =   _context.
                         Categories
                         .AsNoTracking()
                        .AsQueryable();

            var totalCount = await queryable.CountAsync();

            var items=await queryable.
                     OrderBy(category=>category.Id)
                    .Skip((query.PageNumber-1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync();
            return new PagedResult<Category> 
            { 
            Items=items,
            TotalCount=totalCount,
             PageNumber=query.PageNumber,
             PageSize=query.PageSize
              
            };
        }

      
    }
}
