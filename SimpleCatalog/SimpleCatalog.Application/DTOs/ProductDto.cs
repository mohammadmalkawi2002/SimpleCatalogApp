using SimpleCatalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }= string.Empty;

    }

    public class CreateProductDto 
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }


    public class UpdateProductDto 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

    }
}
