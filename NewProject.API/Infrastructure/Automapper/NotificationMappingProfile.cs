using AutoMapper;
using NewProject.API.Requests.Machine;
using NewProject.API.Requests.Notification;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.Notification;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.Notification;

namespace NewProject.API.Infrastructure.Automapper
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<GetNotificationRequest, GetNotificationRequestDto>();
            CreateMap<Notification, GetNotificationDto>().ReverseMap();
        }

    }
}
