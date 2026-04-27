using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Domain.Entities
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public virtual User Sender { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public virtual User Receiver { get; set; }
    }
}
