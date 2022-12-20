using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class PaymentRepository<TContext> : BaseRepository<Payment, TContext>, IPaymentRepository<TContext> where TContext : IBaseContext
    {
        public PaymentRepository(TContext unit) : base(unit)
        {

        }
    }
}
