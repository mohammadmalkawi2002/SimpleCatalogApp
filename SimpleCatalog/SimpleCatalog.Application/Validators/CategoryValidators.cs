using FluentValidation;
using SimpleCatalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Validators
{
    public class CategoryValidators : AbstractValidator<CreateCategoryDto>
    {

        public CategoryValidators()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Category Name is required")
                .MaximumLength(100).WithMessage("ProductName Should be less than 100 characters");


        }
    }

    public class UpdateCategoryDtoValidator
    : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Category Name is required")
                .MaximumLength(100).WithMessage("ProductName Should be less than 100 characters");





        }
    }
}