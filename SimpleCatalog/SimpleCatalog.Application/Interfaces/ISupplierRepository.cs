using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier> AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);

        Task<PagedResult<Supplier>> GetPagedResultAsync(int pageNumber, int pageSize);
        Task DeleteAsync(int id);
    }
}
