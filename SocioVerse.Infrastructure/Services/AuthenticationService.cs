using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SocioVerse.Application.DTOs;
using SocioVerse.Application.Interfaces;
using SocioVerse.Infrastructure.Repositorires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWTService _jwtService;

        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, IJWTService jwtService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<AuthResponseDTO> LoginAync(LoginDTO login)
        {
            var user = await _userService.GetUserByUserName(login.Username) 
                ?? throw new Exception("Invalid Credentials");

            bool isPasswordValid = _passwordHasher.Verify(login.Password, user.Password);

            if (!isPasswordValid) throw new Exception("Invalid Credentials");

            var result = await IssueTokenPairAsync(user.UserId, user.Email);

            return result;
        }

        public async Task LogoutAsync(LogoutDTO logout)
        {
            await _userService.RevokeActiveRefreshToken(logout.UserId, logout.RefreshToken);
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenDTO refreshToken)
        {
            var claims = _jwtService.ParseAccessToken(refreshToken.AccessToken, true)
                ?? throw new Exception("Invalid Access token");

            var storedToken = await _userService.FindActiveRefreshToken(claims.UserId, refreshToken.RefreshToken)
                ?? throw new Exception("Invalid Refresh token");

            await _userService.RevokeActiveRefreshToken(claims.UserId, storedToken);

            return await IssueTokenPairAsync(claims.UserId, claims.Email);
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO register)
        {
            var exists = await _userService.UserExists(register.Username);

            if (exists) throw new Exception($"User with Username {register.Username} already exists");

            string passwordHash = _passwordHasher.Hash(register.Password);

            var user = new UserDTO(0, register.Username, register.Email, passwordHash);

            var userId = await _userService.CreateAsync(user);

            return await IssueTokenPairAsync(userId, register.Email);
        }

        private async Task<AuthResponseDTO> IssueTokenPairAsync(int userId, string email)
        {
            var (authToken, authExpiry) = _jwtService.GenerateAccessToken(userId, email);

            var (refreshToken, refreshExpiry) = _jwtService.GenerateRefreshToken();

            await _userService.SaveActiveRefreshToken(userId, refreshToken, refreshExpiry);

            return new AuthResponseDTO(authToken, refreshToken, authExpiry, refreshExpiry);
        }
    }
}
