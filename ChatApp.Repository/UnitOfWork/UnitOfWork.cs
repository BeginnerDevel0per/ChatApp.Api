using ChatApp.Core.UnifOfWorks;
using ChatApp.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _DbContext;


        public UnitOfWork(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Commit()
        {
            _DbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
           await _DbContext.SaveChangesAsync();
        }
    }
}
