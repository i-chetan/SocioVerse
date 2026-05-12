using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocioVerse.Infrastructure.Services
{
    public sealed class JWTOptions
    {
        public const string Section = "JWT";
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpiryInMins { get; set; } = 15;
        public int RefreshTokenExpiryInDays { get; set; } = 3;

    }
    public sealed class JWTService(IOptions<JWTOptions> opts) : IJWTService
    {
        public readonly JWTOptions _options = opts.Value;
        public (string token, DateTime expiry) GenerateAccessToken(int userId, string email)
        {
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, userId.ToString()),
                new (JwtRegisteredClaimNames.Email, email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryInMins);

            var jwt = new JwtSecurityToken(issuer: _options.Issuer, audience: _options.Audience, claims: claims, notBefore: DateTime.UtcNow, expires: expiry, signingCredentials: creds);
            return ( new JwtSecurityTokenHandler().WriteToken(jwt), expiry);
        }

        public (string token, DateTime expiry) GenerateRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var expiry = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiryInDays);
            return (token, expiry);
        }
    }

    public interface IJWTService {
        (string token, DateTime expiry) GenerateAccessToken(int userId, string email);
        (string token, DateTime expiry) GenerateRefreshToken(); 
    }

    public class PasswordHasher : SocioVerse.Application.Interfaces.IPasswordHasher
    {
        public string Hash(string plain) => BCrypt.Net.BCrypt.HashPassword(plain, workFactor: 12);

        public bool Verify(string plain, string hash) => BCrypt.Net.BCrypt.Verify(plain, hash);
    }
}
