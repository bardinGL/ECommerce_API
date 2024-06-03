using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Entities
{
    public partial class Checkout
    {
        public Checkout()
        {
            BoughtItems = new HashSet<BoughtItem>();
        }

        public int CheckoutId { get; set; }
        public int? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ShippingMethod { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<BoughtItem> BoughtItems { get; set; }
    }
}
