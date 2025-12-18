
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Application.Services;
using SimpleCatalog.Application.Validators;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;
        private readonly IValidator<BaseQueryParametersRequest> _queryValidator;




        public ProductsController(ProductService productService, IValidator<CreateProductDto> createValidator, IValidator<UpdateProductDto> updateValidator, IValidator<BaseQueryParametersRequest> queryValidator)

        {
            _updateValidator = updateValidator;
            _createValidator = createValidator;

            _productService = productService;
            _queryValidator = queryValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {

           
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }




        [HttpGet("paged")]
        public async Task<ActionResult<List<ProductDto>>> GetPaged([FromQuery]
            ProductQueryParametersRequest request)
        {
            var validationResult=_queryValidator.Validate(request);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors);
            }
            var pagedResult = await _productService.GetPagedProductsAsync(request);

            // Add metadata in headers
            var metadata = new 
            {
               pagedResult.TotalCount,
              pagedResult.PageNumber,
              pagedResult.PageSize,
              pagedResult.TotalPages,
              pagedResult.HasNext,
              pagedResult.HasPrevious,


            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            
            return Ok(pagedResult.Items);
        }

       

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
        {
            //Apply FluentValidation For user Inputs Manually:
            ValidationResult result = _createValidator.Validate(dto);
           
            if (!result.IsValid)
            {
                var errorResponse = result.Errors.Select(e => new
                {
                    PropertyName = e.PropertyName,
                    Error = e.ErrorMessage,
                     ErrorCode=e.ErrorCode,
                    attemptedValue=e.AttemptedValue
                });
                return BadRequest(new { Errors = errorResponse });

            }


            var product = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductDto dto)
        {

            if (id != dto.Id)
                return BadRequest();

           ValidationResult result = await _updateValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var errorResponse = result.Errors.Select(e => new
                {
                    PropertyName = e.PropertyName,
                    Error = e.ErrorMessage,
                    ErrorCode = e.ErrorCode,
                    attemptedValue = e.AttemptedValue
                });
                return BadRequest(new { Errors = errorResponse });
            }


            await _productService.UpdateProductAsync(dto);
                return NoContent();
            
            
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
