using Microsoft.EntityFrameworkCore;
using SocioVerse.Application.DTOs;
using SocioVerse.Application.Interfaces;
using SocioVerse.Application.Persistence;
using SocioVerse.Infrastructure.Persistence;

namespace SocioVerse.Infrastructure.Repositorires
{
    public class UserRepository : IUserService
    {
        private readonly SocialMediaDbContext _context;
        public UserRepository (SocialMediaDbContext context) => _context = context;

        public async Task CreateAsync(UserDTO user)
        {
            var userEntity = UserConversion.ToEntity(user);

            _context.Users.Add(userEntity);

            await _context.SaveChangesAsync();
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
    }
}
