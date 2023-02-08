using Microsoft.AspNetCore.SignalR;

namespace NewProject.API.Hubs
{
    public class ChatHub : Hub
    {

        public async Task SendMessageToAll(string connectionId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", connectionId, message);
        }
        public Task SendMessageToClient(string connectionId, string message)
        {
            connectionId = Context.ConnectionId;
            return Clients.Client(connectionId).SendAsync("ReceiveMessageconectionid", message);
        }


    }


}
