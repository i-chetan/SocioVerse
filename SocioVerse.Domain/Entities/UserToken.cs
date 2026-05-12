using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Domain.Entities
{
    [Table(name: "tbl_UserToken")]
    public class UserToken
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime RevokedAt { get; set; }
    }
}
