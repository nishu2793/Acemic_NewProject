using AutoMapper;
using Microsoft.Extensions.Options;
using NewProject.API.Infrastructure.Models;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Services.Entities.Notification;
using NewProject.Services.Interfaces;
using PushNotification.Models;
using static PushNotification.Models.GoogleNotification;
using System.Net.Http.Headers;
using System.Runtime;
using CorePush.Google;
using Newtonsoft.Json.Linq;
using CorePush.Utils;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Threading;

namespace NewProject.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        private readonly HttpClient http;

        public NotificationService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             ReadWriteApplicationDbContext readWriteUnitOfWorkSP, IOptions<FcmNotificationSetting> settings)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
            _fcmNotificationSetting = settings.Value;
        }
        public async Task<List<GetNotificationDto>> GetNotification(GetNotificationRequestDto request)
        {
            var data = (from notificationTB in _readOnlyUnitOfWork.NotificationRepository.GetAllAsQuerable()
                        where notificationTB.UserId == request.UserId &&
                        notificationTB.Type == request.Type && notificationTB.ReadOn == null
                        select new GetNotificationDto
                        {
                            Did = notificationTB.Did,
                            Data = notificationTB.Data,
                            UserId = notificationTB.Did,
                            IsRead = notificationTB.IsRead,
                            ReadOn = DateTime.UtcNow,
                            Type = notificationTB.Type
                        }).ToList();

            foreach (var item in data)
            {
                var objNotify = _readWriteUnitOfWork.NotificationRepository.Find(x => x.Did == item.Did).FirstOrDefault();
                objNotify.ReadOn = DateTime.UtcNow;
            }
            await _readWriteUnitOfWork.CommitAsync();
            return data;
        }
        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */

                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}
