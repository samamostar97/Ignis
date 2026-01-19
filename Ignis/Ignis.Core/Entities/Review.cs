using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Review : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(2000)]
        public string? Comment { get; set; }

        // Navigation
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
