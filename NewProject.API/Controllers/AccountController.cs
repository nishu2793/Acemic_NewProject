using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewProject.API.Hubs;
using NewProject.API.Requests.Payment;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.Payment;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHubContext<ChatHub> _hubContext;
        public AccountController(IAuthenticationService authenticationService, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        [HttpPost("authenticate")]
        public async Task<Dictionary<string, object>> AuthenticateAsync([FromBody] UserAuthRequest request)
        {
            var userDto = _mapper.Map<UserAuthRequest, UserAuthRequestDto>(request);
            var result = await _authenticationService.AuthenticateAsync(userDto, GetIdAddress());

            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        #region Helper
        private string GetIdAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        #endregion
        [HttpGet("signalRTest")]
        public async Task<string> aa(string user, string message)
        {
            SignalRclass signalRclass = new SignalRclass()
            {
                user = user,
                message = message
            };
            var json = JsonConvert.SerializeObject(signalRclass);

           // SendAsync("broadcastAppointment", JsonConvert.SerializeObject(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            var connection = new NewProject.API.Hubs.ChatHub();
            await _hubContext.Clients.All.SendAsync(json);
            return message;
        }
        public class SignalRclass
        {
            public string user { get; set; }
            public string message { get; set; }
        }

    }
}
