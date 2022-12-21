using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.Machine;
using NewProject.API.Requests.Order;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Services.Services;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
      

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost("GetOrder")]
        public async Task<Dictionary<string, object>> GetOrder([FromBody] GetOrderRequest request)
        {
            var orderdto = _mapper.Map<GetOrderRequest, GetOrderDto>(request);
            var result = await _orderService.GetOrder(orderdto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetAllOrder")]
        public async Task<Dictionary<string, object>> GetAllOrder()
        {
            var result = await _orderService.GetAllOrder();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("SaveOrder")]
        public async Task<Dictionary<string, object>> SaveOrder([FromBody] SaveOrderRequest request)
        {
            var saveorderDto = _mapper.Map<SaveOrderRequest, SaveOrderDto>(request);
            var result = await _orderService.SaveOrder(saveorderDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("UpdateOrder")]
        public async Task<Dictionary<string, object>> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            var updateorderDto = _mapper.Map<UpdateOrderRequest, UpdateOrderDto>(request);
            var result = await _orderService.UpdateOrder(updateorderDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("UpdateStatus")]
        public async Task<Dictionary<string, object>> UpdateStatus([FromBody] UpdateOrderRequest request)
        {
           var updateorderDto = _mapper.Map<UpdateOrderRequest, UpdateOrderDto>(request);
            var result = await _orderService.UpdateStatus(updateorderDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("DeleteOrder")]
        public async Task<Dictionary<string, object>> Deleteorder([FromBody] DeleteOrderRequest request)
        {
            var deleteorder = _mapper.Map<DeleteOrderRequest, DeleteOrderDto>(request);
            var result = await _orderService.DeleteOrder(deleteorder);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

    }
}

