using SocioVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDTO> LoginAync(LoginDTO login);
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO register);

        Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenDTO refreshToken);
        Task LogoutAsync(LogoutDTO logout);
    }
}
