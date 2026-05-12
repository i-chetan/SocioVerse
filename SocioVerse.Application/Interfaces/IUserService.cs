using SocioVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserDTO user);
        Task UpdateAsync(UserDTO user);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO> GetUserByUserName(string userName);
    }
}
