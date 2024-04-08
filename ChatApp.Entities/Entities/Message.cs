using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Entities.Entities
{
    public class Message : BaseEntity
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Content { get; set; }
        public string ConversationId { get; set; }
    }
}
