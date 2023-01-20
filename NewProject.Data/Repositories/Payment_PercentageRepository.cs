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
    public class Payment_PercentageRepository<TContext> : BaseRepository<Payment_Percentage, TContext>, IPayment_PercentageRepository<TContext> where TContext : IBaseContext
    {
        public Payment_PercentageRepository(TContext unit) : base(unit)
        {

        }
    }
}
