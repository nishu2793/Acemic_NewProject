using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.SignalR;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class SignalRRepository<TContext> : BaseRepository<SignalR, TContext>, ISignalRRepository<TContext> where TContext : IBaseContext
    {
        public SignalRRepository(TContext unit) : base(unit)
        {

        }
    }
}
