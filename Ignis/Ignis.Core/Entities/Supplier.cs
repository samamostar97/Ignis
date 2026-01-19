using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Supplier : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? ContactEmail { get; set; }

        [MaxLength(20)]
        public string? ContactPhone { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        // Navigation
        public ICollection<Product> Products { get; set; } = [];
    }
}
