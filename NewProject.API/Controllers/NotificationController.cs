using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.Notification;
using NewProject.API.Requests.Order;
using NewProject.Services.Entities.Notification;
using NewProject.Services.Entities.Order;
using NewProject.Services.Interfaces;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;


        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService= notificationService;
            _mapper = mapper;
        }
        [HttpPost("GetNotification")]
        public async Task<Dictionary<string, object>> GetNotification([FromBody] GetNotificationRequest request)
        {
            var noticationdto = _mapper.Map<GetNotificationRequest, GetNotificationRequestDto>(request);
            var result = await _notificationService.GetNotification(noticationdto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

    }
}
