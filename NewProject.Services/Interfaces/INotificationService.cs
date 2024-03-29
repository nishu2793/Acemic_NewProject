﻿using NewProject.API.Infrastructure.Models;
using NewProject.Services.Entities.Notification;
using PushNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<GetNotificationDto>> GetNotification(GetNotificationRequestDto request);
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
