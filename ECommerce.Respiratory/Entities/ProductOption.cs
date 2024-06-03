using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Entities
{
    public partial class ProductOption
    {
        public ProductOption()
        {
            BoughtItems = new HashSet<BoughtItem>();
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int OptionId { get; set; }
        public int? ProductId { get; set; }
        public string OptionName { get; set; } = null!;
        public string OptionValue { get; set; } = null!;

        public virtual Product? Product { get; set; }
        public virtual ICollection<BoughtItem> BoughtItems { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
