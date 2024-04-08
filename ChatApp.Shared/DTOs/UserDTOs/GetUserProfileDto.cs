using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs.UserDTOs
{
    public class GetUserProfileDto
    {
        public FriendOfUsersDto? FriendOfUsersDto { get; set; }

        public UserDto UserDto { get ; set; }

        
    }
}
