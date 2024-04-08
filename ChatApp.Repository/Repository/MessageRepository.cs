using ChatApp.Core.Repositories;
using ChatApp.Entities.Entities;
using ChatApp.Repository.Context;
using ChatApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Repository.Repository
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Message> GetLastMessages(Guid UserId)
        {

            var LastMessages = _dbContext.Messages.Include(x => x.Sender).Include(x => x.Receiver)
                .Where(m => m.SenderId == UserId || m.ReceiverId == UserId)
                .OrderByDescending(x => x.CreatedDate)
                .GroupBy(m => m.ConversationId)
                .Select(x => x.OrderByDescending(x => x.CreatedDate).First())
                .AsNoTracking().AsQueryable();
            return LastMessages;
        }



        public IQueryable<Message> GetMessagesBetweenToUsers(Guid UserId, Guid OtherUserId)
        {

            var messages = _dbContext.Messages.Where(m => (m.SenderId == UserId && m.ReceiverId == OtherUserId) || (m.SenderId == OtherUserId && m.ReceiverId == UserId)).AsNoTracking().AsQueryable();
            return messages;
        }
    }
}
