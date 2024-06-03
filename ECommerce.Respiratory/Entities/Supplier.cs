using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Entities
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
