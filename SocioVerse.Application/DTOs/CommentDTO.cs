using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.DTOs
{
    public record CommentDTO(
        int Id,
        int PostId,
        int UserId,
        string Content,
        DateTime CreatedAt);
}
