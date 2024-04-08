using ChatApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Repositories
{
    public interface ILoginRepository : IGenericRepository<User>
    {
        Task<User?> Login(string UserName, string Password);
    }
}
