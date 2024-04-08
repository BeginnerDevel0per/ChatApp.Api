using ChatApp.Core.Repositories;
using ChatApp.Entities.Entities;
using ChatApp.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Repository.Repository
{
    public class LoginRepository : GenericRepository<User>, ILoginRepository
    {
        public LoginRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> Login(string UserName, string Password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == UserName && x.Password == Password);
        }

       
    }
}
