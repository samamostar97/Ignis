using Ignis.Application.DTOs.Auth;

namespace Ignis.Application.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshRequest request);
    }
}
