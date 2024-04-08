using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs
{
    public class MessagesDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; } 

        public DateTime? UpdateDate { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiveId { get; set; }
        public string Content { get; set; }
    }
}
