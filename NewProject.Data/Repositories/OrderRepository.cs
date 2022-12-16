using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class OrderRepository<TContext> : BaseRepository<Order, TContext>, IOrderRepository<TContext> where TContext : IBaseContext
    {
        public OrderRepository(TContext unit) : base(unit)
        {

        }

    }
}
