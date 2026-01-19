using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }

        [MaxLength(256)]
        public string? StripePaymentIntentId { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty; // "Pending", "Succeeded", "Failed", "Refunded"

        // Navigation
        public Order Order { get; set; } = null!;
    }
}
