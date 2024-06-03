using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Respiratory.Entities
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public int? OptionId { get; set; }

        public virtual ProductOption? Option { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
