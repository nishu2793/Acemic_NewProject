using NewProject.Data.Contexts;
using NewProject.Domain.Entities.Notification;
using NewProject.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories.Interfaces
{
    public interface INotificationRepository<TContext> : IBaseRepository<Notification, TContext> where TContext : IBaseContext
    {

    }
}
