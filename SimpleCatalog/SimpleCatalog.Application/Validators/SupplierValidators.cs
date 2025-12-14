using FluentValidation;
using SimpleCatalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Validators
{
    public class CreateSupplierDtoValidator : AbstractValidator<CreateSupplierDto>

    {
    public CreateSupplierDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("please enter a valid email address");

            
            

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(50);
        }
    }


    public class UpdateSupplierDtoValidator
    : AbstractValidator<UpdateSupplierDto>
    {
        public UpdateSupplierDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("please enter a valid email address");




            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(50);

        }
    }
    
}
