using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        // Foreign keys
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        // Navigation
        public ProductCategory Category { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<WishlistItem> WishlistItems { get; set; } = [];
    }
}
