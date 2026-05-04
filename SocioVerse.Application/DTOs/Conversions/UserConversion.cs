using SocioVerse.Application.DTOs;
using SocioVerse.Domain.Entities;

namespace SocioVerse.Application.Persistence
{
    public static class UserConversion
    {
        public static User ToEntity(UserDTO user) => new User()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            PasswordHash = user.Password,
            Email = user.Email,
        };

        public static (UserDTO?, IEnumerable<UserDTO>?) FromEntity(User user, IEnumerable<User>? users)
        {
            if (user is not null || users is null)
            {
                var singleUser = new UserDTO(user!.UserId, user.UserName, user.Email, user.PasswordHash);

                return (singleUser, null);
            }

            if (users is not null || user is null)
            {
                var _users = users!.Select(x => new UserDTO(x.UserId, x.UserName, x.Email, x.PasswordHash)).ToList();

                return (null, _users);
            }

            return (null , null);
        }
    }
}
