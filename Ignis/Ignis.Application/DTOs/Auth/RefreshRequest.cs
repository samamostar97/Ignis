using System.ComponentModel.DataAnnotations;

namespace Ignis.Application.DTOs.Auth
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
