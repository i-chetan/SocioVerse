using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Domain.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual ICollection<Like> Likes { get; set; } = [];

    }
}
