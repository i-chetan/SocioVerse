using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.DTOs
{
    public record PostDTO(
        int PostId,
        [Required] string Content,
        DateTime CreatedAt,
        [Required] int AuthorId
    );
}
