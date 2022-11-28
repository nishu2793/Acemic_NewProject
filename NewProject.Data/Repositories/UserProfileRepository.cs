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
    public class UserProfileRepository<TContext> : BaseRepository<UserRegister, TContext>, IUserProfileRepository<TContext> where TContext : IBaseContext
    {
        public UserProfileRepository(TContext unit) : base(unit)
        {

        }
    }
}
