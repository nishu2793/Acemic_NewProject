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
    public class UserRegisterTempRepository<TContext> : BaseRepository<UserRegisterTemp, TContext>, IUserRegisterTempRepository<TContext> where TContext : IBaseContext
    {
        public UserRegisterTempRepository(TContext unit) : base(unit)
        {

        }
    }
}
