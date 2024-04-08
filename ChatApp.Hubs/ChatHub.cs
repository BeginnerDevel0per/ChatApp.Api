using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace ChatApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        static List<string> ClientsConnection = new();

        //public async Task SendMessage(string ConnectionId, string message)
        //{
        //    await Clients.Others.SendAsync("ReceiveMessage", message);
        //    await Clients.Client(ConnectionId).SendAsync("ReceiveMessage", ConnectionId, message);
        //    await Clients.Users(ConnectionId).SendAsync(ConnectionId, message);
        //    await Clients.Caller.SendAsync(message);

        //    await Clients.Others.SendAsync(message);
        //}


        public override async Task OnConnectedAsync()
        {
            var userConnection = Context.UserIdentifier;
            if (!ClientsConnection.Any(x => x == userConnection))
            {
                ClientsConnection.Add(userConnection);
            }

            await Clients.All.SendAsync("UserOnlineState", ClientsConnection);
            //await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userConnection = Context.UserIdentifier;
            if (ClientsConnection.Any(x => x == userConnection))
            {
                ClientsConnection.Remove(userConnection);
            }
            await Clients.All.SendAsync("UserOnlineState", ClientsConnection);
            //await base.OnDisconnectedAsync(exception);
        }
        public async Task IsOnlineUsers()
        {
            await Clients.All.SendAsync("UserOnlineState", ClientsConnection);
        }
    }
}
