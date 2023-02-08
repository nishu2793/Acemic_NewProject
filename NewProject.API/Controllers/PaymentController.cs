using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewProject.API.Hubs;
using NewProject.API.Requests.Payment;
using NewProject.API.Requests.SignalR;
using NewProject.Services.Entities.Payment;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using Newtonsoft.Json;

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

        public PaymentController(IMapper mapper, IPaymentService paymentService, IHubContext<ChatHub> hubContext, ISignalRService signalRService)
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
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessageconectionid", connectionId, JsonConvert.SerializeObject(result));
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("UpdatePayment")]
        public async Task<Dictionary<string, object>> UpdatePayment([FromBody] UpdatePaymentRequest request)
        {
            var updatePaymentDto = _mapper.Map<UpdatePaymentRequest, UpdatePaymentDto>(request);
            var result = await _paymentService.UpdatePayment(updatePaymentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        /*        public string? GetUserId(SignalRDto request)
                {
                    var userId = MyCustomUserClass.FindUserId(request.User.Identity.Name);
                    return userId.ToString();
                }*/
    }
}
