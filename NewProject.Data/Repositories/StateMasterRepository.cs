using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class StateMasterRepository<TContext> : BaseRepository<StateMaster, TContext>, IStateMasterRepository<TContext> where TContext : IBaseContext
    {
        public StateMasterRepository(TContext unit) : base(unit)
        {

        }
    }
}
