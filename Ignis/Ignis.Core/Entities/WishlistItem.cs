using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class WishlistItem : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
