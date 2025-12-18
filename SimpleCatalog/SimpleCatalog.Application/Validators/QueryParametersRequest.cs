using FluentValidation;
using SimpleCatalog.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Validators
{
    public class BaseQueryParametersRequestValidator:AbstractValidator<BaseQueryParametersRequest>
    {
        public BaseQueryParametersRequestValidator() 
        {
            RuleFor(q => q.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

            RuleFor(q => q.PageSize)
                .InclusiveBetween(1, 50)
                .WithMessage("PageSize must be between 1 and 50.");

            RuleFor(q => q.SortDirection)
                .Must(sd => sd == null || sd.ToLower() == "asc" || sd.ToLower() == "desc")
                .WithMessage("SortDirection must be either 'asc' or 'desc'.");
        }


    }

    public class ProductQueryParametersValidator:
        AbstractValidator<ProductQueryParametersRequest>
    {
        public ProductQueryParametersValidator()
        {
            Include(new BaseQueryParametersRequestValidator());

            RuleFor(x => x.SortBy)
            .Must(x =>
                x == null ||
                x == "Name" ||
                x == "Price" ||
                x == "CreatedAt");

            RuleFor(x => x.SearchBy)
                .Must(x => x == null || x == "Name" || x == "Price");
        }
    }
}
