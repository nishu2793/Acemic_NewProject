using NewProject.Data.Contexts;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories.Interfaces
{
    public interface IPaymentRepository<TContext> : IBaseRepository<Payment, TContext> where TContext : IBaseContext
    {

    }
}
