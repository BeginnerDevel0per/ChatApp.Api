using ChatApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
    public interface IValidateTokenService : IGenericService<User>
    {
        bool ValidateToken(User user);
    }
}
