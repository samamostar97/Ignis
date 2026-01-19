using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Faq : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Question { get; set; } = string.Empty;

        [Required]
        [MaxLength(5000)]
        public string Answer { get; set; } = string.Empty;
    }
}
