using SimpleCatalog.Domain.Common;
using SimpleCatalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }

        // Navigation property
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }=null!;
    }
}
