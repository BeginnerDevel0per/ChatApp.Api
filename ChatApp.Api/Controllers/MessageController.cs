using ChatApp.Core.Services;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.AccessControl;

namespace ChatApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _MessageService;
        public MessageController(IMessageService messageService)
        {
            _MessageService = messageService;

        }
        [HttpGet]
        [Route("/Message/{SentUserId}/{message}")]
        public async Task<IActionResult> Message(Guid SentUserId, string message)
        {
            await _MessageService.SendMessage(SentUserId, message);
            return Ok();
        }

        [HttpGet]
        [Route("/LastMessages")]
        public IActionResult GetLastMessages()
        {
            var LastMessages = _MessageService.GetLastMessages();
            return Ok(LastMessages);
        }
        [HttpGet]
        [Route("/GetMessagesBetweenToUsers/{OtherUserId}")]
        public IActionResult GetMessagesBetweenToUsers(Guid OtherUserId)
        {
            var Messages = _MessageService.GetMessagesBetweenToUsers(OtherUserId);
            return Ok(Messages);
        }
    }
}
