using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Application.Pagination
{
    public class BaseQueryParametersRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc";
        public string? SearchBy { get; set; }
    }


   public class ProductQueryParametersRequest : BaseQueryParametersRequest
    {
       
    }

    public class CategoryQueryParametersRequest : BaseQueryParametersRequest
    {

    }

}
