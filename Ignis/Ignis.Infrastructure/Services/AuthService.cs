using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ignis.Application.DTOs.Auth;
using Ignis.Application.IServices;
using Ignis.Core.Entities;
using Ignis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ignis.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private const int AccessTokenExpiryHours = 24;
        private const int RefreshTokenExpiryDays = 7;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? throw new InvalidOperationException("JWT_SECRET nije konfigurisan");
            _jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
                ?? "Ignis";
            _jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
                ?? "IgnisApp";
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Korisnik sa ovim email-om vec postoji.");
            }

            // Create new user
            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate tokens
            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && !u.IsDeleted);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Neispravan email ili password");
            }

            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshRequest request)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

            if (refreshToken == null)
            {
                throw new InvalidOperationException("Neispravan refresh token");
            }

            if (!refreshToken.IsActive)
            {
                throw new InvalidOperationException("Refresh token je istekao ili je revoked");
            }

            // Revoke the old refresh token
            refreshToken.RevokedAt = DateTime.UtcNow;

            // Generate new tokens
            return await GenerateAuthResponse(refreshToken.User);
        }

        private async Task<AuthResponse> GenerateAuthResponse(User user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshToken(user.Id);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddHours(AccessTokenExpiryHours),
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role
                }
            };
        }

        private string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(AccessTokenExpiryHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<RefreshToken> GenerateRefreshToken(int userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays),
                CreatedAt = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
