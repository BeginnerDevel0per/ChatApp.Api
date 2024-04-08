using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Entities.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }

        public DateTime SendDate { get; set; } = DateTime.Now;

        public Guid SendRequestId { get; set; }

        public Guid UserId { get; set; }
    }
}
