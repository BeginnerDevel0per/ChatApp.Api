using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Entities.Entities
{
    public class FriendsOfUser
    {
        public int Id { get; set; }

        public DateTime RequestSenderDate { get; set; } = DateTime.Now;

        public DateTime? RequestAcceptDate { get; set; } 
        public Guid FriendOrRequestId { get; set; }

        public Guid UserId { get; set; }

        public bool IsRequestAccepted { get; set; }


        public User FriendOrRequestUser { get; set; }

        public User User { get; set; }
    }
}
