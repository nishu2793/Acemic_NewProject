using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewProject.API.Hubs;
using NewProject.API.Requests.SignalR;
using NewProject.Services.Entities.SignalR;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using Newtonsoft.Json;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISignalRService _signalRService;
        private IHubContext<ChatHub> _hubContext;
        public SignalRController(ISignalRService signalRService, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _mapper = mapper;
            _signalRService= signalRService;
            _hubContext = hubContext;
        }

        [HttpPost("GetSignalR")]
        public async Task<Dictionary<string, object>> GetSignalR([FromBody] GetSignalRRequest request)
        {
            var signaldto = _mapper.Map<GetSignalRRequest, GetSignalRDto>(request);
            var result = await _signalRService.GetSignalR(signaldto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("GetAllSignalR")]
        public async Task<Dictionary<string, object>> GetAllUserRegister()
        {
            var result = await _signalRService.GetAllSignalR();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        
        [HttpPost("SaveSignalR")]
        public async Task<Dictionary<string, object>> SaveSignalR([FromBody] SaveSignalRRequest request)
        {
            var SaveSignalRDto = _mapper.Map<SaveSignalRRequest, SaveSignalDto>(request);
            var result = await _signalRService.SaveSignalR(SaveSignalRDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("signalRConnectionID")]
        public async Task<string> signalRConnectionID(string connectionId, string message)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessageconectionid", connectionId, message);
            return JsonConvert.SerializeObject(message);
        }
        [HttpGet("signalRTest")]
        public async Task<string> signalRTest(string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message).ConfigureAwait(false);

            ConnectedUser.UserId.Count();
            return message;
        }

        [HttpPost("signalRJSON")]
        public async Task<string> signalRJSON(string user, SignalRRequest signalRJSON)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveJson", user, signalRJSON).ConfigureAwait(false);
            return JsonConvert.SerializeObject(signalRJSON);
        }
    }
}
