using SimpleCatalog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Domain.Entities
{
    public class Supplier:BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }=string.Empty;
        public string Phone { get; set; }=string.Empty;

        // Navigation property: One supplier can have many products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
