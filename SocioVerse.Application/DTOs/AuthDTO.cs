using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.DTOs
{
    public record LoginDTO(string Username, string Password);
    public record RegisterDTO(string Username, string Email, string Password);
    public record AuthResponseDTO(string AccessToken, string RefreshToken, DateTime AcessTokenExpiry, DateTime RefreshTokenExpiry);
    public record LogoutDTO(int UserId, string RefreshToken);
    public record RefreshTokenDTO(string AccessToken, string RefreshToken);
}
