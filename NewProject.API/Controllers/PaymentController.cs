using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewProject.API.Hubs;
using NewProject.API.Requests.Order;
using NewProject.API.Requests.Payment;
using NewProject.API.Requests.SignalR;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.Payment;
using NewProject.Services.Entities.SignalR;
using NewProject.Services.Interfaces;
using NewProject.Services.Services;
using NewProject.Utility;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ISignalRService _signalRService;

        public PaymentController(IMapper mapper, IPaymentService paymentService, IHubContext<ChatHub> hubContext,ISignalRService signalRService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
            _hubContext = hubContext;  
            _signalRService = signalRService;
        }
        [HttpPost("GetPayment")]
        public async Task<Dictionary<string, object>> GetPayment([FromBody] GetPaymentRequest request)
        {
            var paymentdto = _mapper.Map<GetPaymentRequest, GetPaymentDto>(request);
            var result = await _paymentService.GetPayment(paymentdto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetAllPayment")]
        public async Task<Dictionary<string, object>> GetAllPayment()
        {
            var result = await _paymentService.GetAllPayment();
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", "abc", "TestSignalR").ConfigureAwait(false);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage","AAA", "TestSignalR").ConfigureAwait(false);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("SavePayment")]
        public async Task<Dictionary<string, object>> SavePayment([FromBody] SavePaymentRequest request)
        {
            var savepaymentDto = _mapper.Map<SavePaymentRequest, SavePaymentDto>(request);
            var result = await _paymentService.SavePayment(savepaymentDto);
      
            Guid orderid = new Guid(request.Orderid.ToString());
        
            var connectionId = await _signalRService.Connection(orderid);
          //  string connectionId=connectionI.FirstOrDefault()?.ToString();  
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessageconectionid", connectionId, JsonConvert.SerializeObject(result));
        
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        /*        public string? GetUserId(SignalRDto request)
                {
                    var userId = MyCustomUserClass.FindUserId(request.User.Identity.Name);
                    return userId.ToString();
        JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
                }*/
    }
}
