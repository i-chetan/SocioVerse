using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.DTOs
{
    public record UserDTO(int UserId, [Required] string UserName, [Required] string Email, [Required] string Password);
}
