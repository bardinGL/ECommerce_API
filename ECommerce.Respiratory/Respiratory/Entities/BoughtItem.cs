using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Respiratory.Entities
{
    public partial class BoughtItem
    {
        public int BoughtItemId { get; set; }
        public int? CheckoutId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public int? OptionId { get; set; }

        public virtual Checkout? Checkout { get; set; }
        public virtual ProductOption? Option { get; set; }
        public virtual Product? Product { get; set; }
    }
}
