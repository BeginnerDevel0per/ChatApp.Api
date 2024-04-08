using ChatApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Repositories
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        IQueryable<Message> GetLastMessages(Guid UserId);

        IQueryable<Message> GetMessagesBetweenToUsers(Guid UserId, Guid OtherUserId);
    }
}
