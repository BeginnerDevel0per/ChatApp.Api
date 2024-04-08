﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs.UserDTOs
{
    public class UserRegister
    {
   
        public string UserName { get; set; }

        public string Email { get; set; }
       
        public string Password { get; set; }

        public string PasswordAgain { get; set; }

    }
}
