using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; } = string.Empty; // "IGN-001234"

        public int UserId { get; set; }
        public int? ShippingAddressId { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty; // "Pending", "Processing", "Shipped", "Delivered", "Cancelled"

        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public UserAddress? ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
    }
}
