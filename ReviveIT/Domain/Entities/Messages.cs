using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Messages
    {
        [Key]
        public int MessageID { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public DateTime Timestamp { get; set; }

        public string SenderID { get; set; }
        [ForeignKey("SenderID")]
        public Users Sender { get; set; }

        public string RecipientID { get; set; }
        [ForeignKey("RecipientID")]
        public Users Recipient { get; set; }
    }
}
