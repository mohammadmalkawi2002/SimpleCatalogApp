using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Services;

namespace SimpleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SuppliersController(SupplierService supplierService) 
        { 
        _supplierService = supplierService;
        }

        [HttpGet]

        public async Task<ActionResult<List<SupplierDto>>> GetAll() 
        { 
            var suppliers=await _supplierService.GetAllSupplierAsync();

            return Ok(suppliers);
        }


        [HttpGet]
        [Route("{id}")]

        public async Task<ActionResult<SupplierDto>> GetById([FromRoute] int id)
        {
            var supplier= await _supplierService.GetSupplierByIdAsync(id);

            if(supplier is null) 
                return NotFound();

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> Create(CreateSupplierDto dto) 
        { 
            var supplier= await _supplierService.CreateSupplierAsync(dto);

                return CreatedAtAction(nameof(GetById),new {id=supplier.Id},supplier);
        
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSupplierDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _supplierService.UpdateSupplierAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return NoContent();
        }


    }
}
