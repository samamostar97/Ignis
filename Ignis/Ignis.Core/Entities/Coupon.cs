using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Coupon : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty; // "SUMMER20"

        [Required]
        [MaxLength(20)]
        public string DiscountType { get; set; } = string.Empty; // "Percentage" or "FixedAmount"

        public decimal DiscountValue { get; set; }

        public decimal? MinimumOrderAmount { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? MaxUses { get; set; }

        public int TimesUsed { get; set; }

        public bool IsActive { get; set; }
    }
}
