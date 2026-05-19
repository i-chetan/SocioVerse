using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SocioVerse.Application.DTOs;
using SocioVerse.Application.Interfaces;
using SocioVerse.Application.Persistence;
using SocioVerse.Domain.Entities;
using SocioVerse.Infrastructure.Persistence;

namespace SocioVerse.Infrastructure.Repositorires
{
    public class UserRepository : IUserService
    {
        private readonly SocialMediaDbContext _context;
        public UserRepository (SocialMediaDbContext context) => _context = context;

        public async Task<int> CreateAsync(UserDTO user)
        {
            var userEntity = UserConversion.ToEntity(user);

            var _user = _context.Users.Add(userEntity).Entity;

            await _context.SaveChangesAsync();

            return _user.UserId;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var _user = await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

            var (result, _) = UserConversion.FromEntity(_user!, null);

            return result!;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var _users = await _context.Users.ToListAsync();

            var (_, results) = UserConversion.FromEntity(null!, _users);

            return results!;
        }

        public async Task UpdateAsync(UserDTO user)
        {
            var _user = await _context.Users.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync();

            if (_user == null) return;

            _user.UserName = user.UserName;
            _user.Email = user.Email;
            _user.PasswordHash = user.Password;

            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var _user = await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();

            var (user, _) = UserConversion.FromEntity(_user!, null);

            return user;
        }

        public async Task<UserDTO> GetUserByUserName(string userName)
        {
            var _user = await _context.Users.Where(x =>x.UserName == userName).FirstOrDefaultAsync();

            var (user, _) = UserConversion.FromEntity(_user!, null);

            return user!;
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task<string> FindActiveRefreshToken(int userId, string refreshToken)
        {
            var token = await _context.UserTokens.Where(x => !x.IsRevoked &&  x.UserId == userId && x.Token==refreshToken && x.Expiry > DateTime.UtcNow).Select(x => x.Token).FirstOrDefaultAsync();
            return token!;
        }

        public async Task SaveActiveRefreshToken(int userId, string refreshToken, DateTime expiry)
        {
            var token = new UserToken
            {
                Token = refreshToken,
                UserId = userId,
                IsRevoked = false,
                Expiry = expiry,
            };

            _context.UserTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeActiveRefreshToken(int userId, string refreshToken)
        {
            var token = await _context.UserTokens.Where(x => x.UserId==userId && x.Token == refreshToken).FirstOrDefaultAsync();
            if (token is null) return;

            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
