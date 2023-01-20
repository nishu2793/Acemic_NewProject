using Microsoft.AspNetCore.SignalR;
using NewProject.API.Requests.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewProject.API.Hubs
{
    public class SignalRCommunication
    {
        public async Task SendNotification(IHubContext<ChatHub> hubContext)
        {
            SignalRRequest request = new SignalRRequest();
            request.ConnectionId = "aa";
            //request.UserId = 7845612345623;
            await hubContext.Clients.All.SendAsync("sendMessageToAll", JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

    }
}
