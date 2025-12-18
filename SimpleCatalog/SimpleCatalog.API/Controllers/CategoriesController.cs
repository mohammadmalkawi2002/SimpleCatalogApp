using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCatalog.Application.DTOs;
using SimpleCatalog.Application.Pagination;
using SimpleCatalog.Application.Services;
using System.Text.Json;

namespace SimpleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;


        public CategoriesController(CategoryService categoryService, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator)
        {
            _categoryService = categoryService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("paged")]

        public async Task<ActionResult<List<CategoryDto>>> GetPagedAsync([FromQuery] BaseQueryParametersRequest request)
        {
            var pagedResult= await _categoryService.GetPagedCategoriesAsync(request);
            var metaData = new 
            {
                pagedResult.TotalCount,
                pagedResult.PageNumber,
                pagedResult.PageSize,
                pagedResult.TotalPages,
                pagedResult.HasNext,
                pagedResult.HasPrevious,


            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
            return Ok(pagedResult.Items);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        {
            var validationResult= _createValidator.Validate(dto);
            if (!validationResult.IsValid) 
            {
                var errorResponse = validationResult.Errors.Select(e => new 
                { 
                 PropertyName=e.PropertyName,
                    ErrorMessage = e.ErrorMessage,
                    AttemptedValue = e.AttemptedValue
                });

                return BadRequest(  new { Errors = errorResponse });
            }
            var category = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var validationResult= _updateValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errorResponse = validationResult.Errors.Select(e => new
                {
                    PropertyName = e.PropertyName,
                    ErrorMessage = e.ErrorMessage,
                    AttemptedValue = e.AttemptedValue
                });
                return BadRequest(new {Errors=errorResponse});
            }

            
                await _categoryService.UpdateCategoryAsync(dto);
                return NoContent();
            
           
            
              
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
