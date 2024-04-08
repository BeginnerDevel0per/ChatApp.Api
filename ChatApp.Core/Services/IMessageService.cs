using ChatApp.Entities.Entities;
using ChatApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
    public interface IMessageService
    {
        Task SendMessage(Guid SentUserId, string message);

        List<LastMessageDto> GetLastMessages();

        IQueryable<Message> GetMessagesBetweenToUsers(Guid OtherUserId);

    }
}
