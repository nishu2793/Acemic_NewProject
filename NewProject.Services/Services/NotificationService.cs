using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Services.Entities.Notification;
using NewProject.Services.Interfaces;

namespace NewProject.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             ReadWriteApplicationDbContext readWriteUnitOfWorkSP)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
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
                            ReadOn= DateTime.UtcNow,
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
    }
}
