using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.DTOs
{
    public record LikeDTO(
        int Id,
        int Userid,
        int Postid,
        DateTime CreatedAt
    );
}
