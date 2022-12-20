using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.Order;
using NewProject.API.Requests.Payment;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.Payment;
using NewProject.Services.Interfaces;
using NewProject.Services.Services;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public PaymentController(IMapper mapper, IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
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
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("SavePayment")]
        public async Task<Dictionary<string, object>> SavePayment([FromBody] SavePaymentRequest request)
        {
            var savepaymentDto = _mapper.Map<SavePaymentRequest, SavePaymentDto>(request);
            var result = await _paymentService.SavePayment(savepaymentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("UpdatePayment")]
        public async Task<Dictionary<string, object>> UpdatePayment([FromBody] UpdatePaymentRequest request)
        {
            var updatepaymentDto = _mapper.Map<UpdatePaymentRequest, UpdatePaymentDto>(request);
            var result = await _paymentService.UpdatePayment(updatepaymentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("DeletePayment")]
        public async Task<Dictionary<string, object>> DeletePayment([FromBody] DeletePaymentRequest request)
        {
            var deletepayment = _mapper.Map<DeletePaymentRequest, DeletePaymentDto>(request);
            var result = await _paymentService.DeletePaymnet(deletepayment);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
