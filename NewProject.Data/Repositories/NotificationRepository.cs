using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.Notification;
using NewProject.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class NotificationRepository<TContext> : BaseRepository<Notification, TContext>, INotificationRepository<TContext> where TContext : IBaseContext
    {
        public NotificationRepository(TContext unit) : base(unit)
        {

        }

    }
}
