using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}
