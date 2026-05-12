using Microsoft.Extensions.Configuration;
using SocioVerse.Application.DTOs;
using SocioVerse.Application.Interfaces;
using SocioVerse.Infrastructure.Repositorires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IConfiguration _config;
        public readonly IUserService _userService;
        public AuthenticationService(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }
        public Task<AuthResponseDTO> LoginAync(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync(LogoutDTO logout)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenDTO refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDTO> RegisterAsync(RegisterDTO register)
        {
            throw new NotImplementedException();
        }
    }
}
