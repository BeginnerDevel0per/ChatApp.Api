﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {

        }

        public UnauthorizedException(string? message) : base(message)
        {

        }
    }
}
