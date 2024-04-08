using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Entities.Entities
{
    public class MessageAndUser
    {
        public Message Message { get; set; }
        public List<User> Users { get; set; }
    }
}
