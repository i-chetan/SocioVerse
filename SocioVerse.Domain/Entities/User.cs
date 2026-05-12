using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocioVerse.Domain.Entities
{
    [Table(name:"tbl_User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<Message> SendMessages { get; set; } = [];
        public virtual ICollection<Message> ReceivedMessages { get; set; } = [];
        public virtual ICollection<Post> Posts { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
        public virtual ICollection<Like> Likes { get; set; } = [];
    }
}
