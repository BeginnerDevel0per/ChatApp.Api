using ChatApp.Shared.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs
{
    public class LastMessageDto
    {
        public MessageDto Message { get; set; }

        public UserIdAndNameDto User { get; set; }

    }
}
