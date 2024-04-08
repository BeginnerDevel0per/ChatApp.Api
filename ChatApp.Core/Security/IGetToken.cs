using ChatApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Security
{
    public interface IGetToken
    {
        Token CreateToken(Guid UserId,string UserName, string Roll,DateTime? LastUpdatePasswordDate);

    }
}
