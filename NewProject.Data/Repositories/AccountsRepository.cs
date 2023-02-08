using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class AccountsRepository<TContext> : BaseRepository<Account, TContext>, IAccountsRepository<TContext> where TContext : IBaseContext
    {
        public AccountsRepository(TContext unit) : base(unit)
        {

        }

    }
}
