using ChatApp.Entities.Entities;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
    public interface ILoginService : IGenericService<User>
    {
        Task<Token> Login(string UserName, string Password);

        
    }
}
