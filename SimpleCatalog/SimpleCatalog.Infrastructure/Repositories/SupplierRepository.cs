using Microsoft.EntityFrameworkCore;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Domain.Entities;
using SimpleCatalog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers
                .ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers
                    .FirstOrDefaultAsync(s=>s.Id==id);

        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
             await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;

        }

        public async Task DeleteAsync(int id)
        {
            var supplier=await _context.Suppliers.FindAsync(id);
            if (supplier != null) 
            { 
              _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }

       

        public async Task UpdateAsync(Supplier supplier)
        {
           _context.Suppliers.Update(supplier);
           await  _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Supplier>> GetPagedResultAsync(int pageNumber, int pageSize)
        {
           var query= _context.Suppliers
                       .AsNoTracking()
                       //.Include(s=>s.Products)
                    .AsQueryable();

            var totalCount= await query.CountAsync();

            var items= await query
                    .OrderBy(supplier=>supplier.Id)
                    .Skip((pageNumber-1)*pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return new PagedResult<Supplier>
            {
                    Items= items,
                    TotalCount=totalCount,
                    PageNumber=pageNumber,
                    PageSize=pageSize
                         
            };
                     

        }
    }
}
