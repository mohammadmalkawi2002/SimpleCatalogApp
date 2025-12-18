using Microsoft.EntityFrameworkCore;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Domain.Entities;
using SimpleCatalog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SimpleCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {

            return await _context.Products
                        .Include(p => p.Category)
                           .Include(P => P.Supplier)
                            .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                     .Include(p => p.Category)
                     .Include(P => P.Supplier)
                     .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Product> AddAsync(Product product)
        {

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
           
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            

        }

       

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Product>> GetPagedAsync(ProductQueryParametersRequest query)
        {
            //In order To be able to added OrderBy ,Skip, Take (تدريجيا)
            var queryable = _context.Products
                    .AsNoTracking()
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .AsQueryable();


            //filtering

            

            // Then Sorting:
            queryable = query.SortBy switch
            {
                "Name" => query.SortDirection == "desc"
                    ? queryable.OrderByDescending(p => p.Name)
                    : queryable.OrderBy(p => p.Name),

                "Price" => query.SortDirection.ToLower() == "desc"
                    ? queryable.OrderByDescending(p => p.Price)
                    : queryable.OrderBy(p => p.Price),

                "CreatedAt" => query.SortDirection == "desc"
                    ? queryable.OrderByDescending(p => p.CreatedAt)
                    : queryable.OrderBy(p => p.CreatedAt),

                    //default sorting by latest products
                _ => queryable.OrderBy(p => p.Id) 
            };

            //Calculate total count of items
            var totalCount = await _context.Products.AsNoTracking().CountAsync();

            //Finally make pagination :

            var items = await queryable
             .Skip((query.PageNumber - 1) * query.PageSize)
             .Take(query.PageSize)
                     .ToListAsync();

            return new PagedResult<Product>
            {
               
                   Items = items,
                 TotalCount = totalCount,
                  PageNumber = query.PageNumber,
                 PageSize = query.PageSize,
                  


            };
        }
    }
}
