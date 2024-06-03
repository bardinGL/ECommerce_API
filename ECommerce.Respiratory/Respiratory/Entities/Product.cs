using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Respiratory.Entities
{
    public partial class Product
    {
        public Product()
        {
            BoughtItems = new HashSet<BoughtItem>();
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            Posts = new HashSet<Post>();
            ProductOptions = new HashSet<ProductOption>();
        }

        public int ProductId { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Favorite { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<BoughtItem> BoughtItems { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
