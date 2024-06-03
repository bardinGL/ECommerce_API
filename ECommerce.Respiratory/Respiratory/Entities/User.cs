using System;
using System.Collections.Generic;

namespace ECommerce.Respiratory.Respiratory.Entities
{
    public partial class User
    {
        public User()
        {
            CartItems = new HashSet<CartItem>();
            Checkouts = new HashSet<Checkout>();
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? AvatarPath { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Checkout> Checkouts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
