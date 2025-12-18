using FluentValidation;
using SimpleCatalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage(" {PropertyName} is required")
                .MaximumLength(200)
                .WithMessage("{PropertyName} Should be less than 200 characters");

            RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price must greater than 0");

            RuleFor(p => p.Price).InclusiveBetween(0, 1000000);
            RuleFor(p => p.Description).MaximumLength(1000);


            RuleFor(x => x.Status)
             .IsInEnum()
          .WithMessage("Invalid product status.");



            RuleFor(p => p.CategoryId)
            .GreaterThan(0);

            RuleFor(p=>p.SupplierId)
                .GreaterThan(0);
        }
    }

    
     public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>

    {
        public UpdateProductDtoValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id cannot be negative or  empty");
            RuleFor(p => p.Name).NotEmpty()
            .WithMessage("The product Name is required")
            .MaximumLength(200)
            .WithMessage("ProductName Should be less than 200 characters");

            RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price must greater than 0");

            RuleFor(p => p.Description).MaximumLength(1000);


            RuleFor(x => x.Status)
             .IsInEnum()
          .WithMessage("Invalid product status.");



            RuleFor(p => p.CategoryId)
            .GreaterThan(0);

            RuleFor(p => p.SupplierId)
                .GreaterThan(0);
        }
        }
    }


