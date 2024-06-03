using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Entities
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
