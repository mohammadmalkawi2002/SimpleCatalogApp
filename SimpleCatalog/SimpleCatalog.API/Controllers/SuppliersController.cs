using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Application.Services;

namespace SimpleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierService _supplierService;
        private readonly IValidator<CreateSupplierDto> _createValidator;
        private readonly IValidator<UpdateSupplierDto> _updateValidator;

        public SuppliersController(SupplierService supplierService,IValidator<CreateSupplierDto> createValidator,IValidator<UpdateSupplierDto> updateValidator) 
        { 
             _supplierService = supplierService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]

        public async Task<ActionResult<List<SupplierDto>>> GetAll() 
        { 
            var suppliers=await _supplierService.GetAllSupplierAsync();

            return Ok(suppliers);
        }



        [HttpGet("paged")]

        public async Task<ActionResult<PagedResult<SupplierDto>>> GetPaged(int pageNumber=1,int pageSize=10) 
        {
            var pagedResult = await _supplierService.GetPagedSuppliersAsync(pageNumber, pageSize);

            return Ok(pagedResult);
        
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
            //Apply FluentValidation For user Inputs Manually:

            ValidationResult validationResult= _createValidator.Validate(dto);
            if (!validationResult.IsValid) 
            {
                var errorRespone = validationResult.Errors.Select(e => new 
                { 
                  PropertyName=e.PropertyName,
                     ErrorMessage = e.ErrorMessage,
                    attemptedValue = e.AttemptedValue

                });

                return BadRequest(new {Errors=errorRespone});
            }

            var supplier= await _supplierService.CreateSupplierAsync(dto);

                return CreatedAtAction(nameof(GetById),new {id=supplier.Id},supplier);
        
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSupplierDto dto)
        {
            if (id != dto.Id) return BadRequest();

            ValidationResult validationResult = _updateValidator.Validate(dto);
            if (!validationResult.IsValid) 
            {
                var errorRespone = validationResult.Errors.Select(e => new
                {
                    PropertyName = e.PropertyName,
                    ErrorMessage = e.ErrorMessage,
                    attemptedValue = e.AttemptedValue

                });
                return BadRequest(new {Errors=errorRespone});
            }
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
