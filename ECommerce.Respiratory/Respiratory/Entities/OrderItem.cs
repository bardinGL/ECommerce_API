using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Respiratory.Entities
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? OptionId { get; set; }

        public virtual ProductOption? Option { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
