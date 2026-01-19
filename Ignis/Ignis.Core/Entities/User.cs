using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ignis.Core.Enums;

namespace Ignis.Core.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public UserRole Role { get; set; } = UserRole.AppUser;

        [MaxLength(256)]
        public string? StripeCustomerId { get; set; }

        // Navigation properties
        public UserAddress? Address { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<WishlistItem> WishlistItems { get; set; } = [];
    }
}
