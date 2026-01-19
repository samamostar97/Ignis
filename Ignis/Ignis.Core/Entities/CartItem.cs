using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignis.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Navigation
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
