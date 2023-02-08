using Microsoft.AspNetCore.SignalR;
using NewProject.API.Requests.SignalR;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.SignalR;

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

        //public Task SendMessage(string message)
        //{zz
        //    return Clients.All.SendAsync("ReceiveMessage", message);
        //}

        //public async Task SendMessageToJSON(string user, SignalRRequest signalRJSON)
        //{
        //    await Clients.All.SendAsync("ReceiveJson", user, signalRJSON);
        //}

        //public string GetConnectionId()
        //{
        //    return Context.ConnectionId;
        //}

        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
        //    await base.OnConnectedAsync();
        //}

        ////public override async Task OnDisconnectedAsync(Exception ex)
        ////{
        ////    await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
        ////    await base.OnDisconnectedAsync(ex);
        ////}
        //public Task JoinGroup(string group)
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, group);
        //}

        //public Task SendMessageToGroup(string group, string message)
        //{
        //    return Clients.Group(group).SendAsync("ReceiveMessage", message);
        //}
    }


}
