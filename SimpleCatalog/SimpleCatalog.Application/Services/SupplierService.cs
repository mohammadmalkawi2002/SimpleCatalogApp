using AutoMapper;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository supplierRepository,IMapper mapper) 
        { 
         _supplierRepository = supplierRepository;
            _mapper = mapper;
        }



        public async Task<List<SupplierDto>> GetAllSupplierAsync() 
        { 
        var suppliers= await _supplierRepository.GetAllAsync();

            return _mapper.Map<List<SupplierDto>>(suppliers);

            //suppliers.Select(s => new SupplierDto
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    Email = s.Email,
            //    Phone = s.Phone

            //}).ToList();
        }

        public async Task<SupplierDto> GetSupplierByIdAsync(int id) 
        { 
        var supplier=await  _supplierRepository.GetByIdAsync(id);

            if (supplier is null)
                return null;

            return _mapper.Map<SupplierDto>(supplier);

        
        }

        public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto dto) 
        {
            var supplier= _mapper.Map<Supplier>(dto);

            supplier.CreatedAt = DateTime.UtcNow;

            var created = await _supplierRepository.AddAsync(supplier);

            return _mapper.Map<SupplierDto>(created);
        
        }

        public async Task UpdateSupplierAsync(UpdateSupplierDto dto) 
        {
            var supplier = await _supplierRepository.GetByIdAsync(dto.Id);

            if (supplier is null)
                throw new Exception("Supplier Not Found");

            _mapper.Map(dto,supplier);
            supplier.UpdatedAt = DateTime.UtcNow;

           await _supplierRepository.UpdateAsync(supplier);
        
        }

        public async Task DeleteSupplierAsync(int id) 
        { 
            await  _supplierRepository.DeleteAsync(id);
        }
    }
}
