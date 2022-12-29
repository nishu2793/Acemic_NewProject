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
            SignalRInternalTest request = new SignalRInternalTest();
            request.User = "aa";
            request.Message = "assad";
            await hubContext.Clients.All.SendAsync("sendMessageToAll", JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

    }
}
