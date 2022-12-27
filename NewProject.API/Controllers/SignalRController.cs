using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.SignalR;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.SignalR;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISignalRService _signalRService;
        public SignalRController(ISignalRService signalRService, IMapper mapper)
        {
            _mapper = mapper;
            _signalRService= signalRService;
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
    }
}
