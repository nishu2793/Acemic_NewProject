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
    public class CityMasterRepository<TContext> : BaseRepository<CityMaster, TContext>, ICityMasterRepository<TContext> where TContext : IBaseContext
    {
        public CityMasterRepository(TContext unit) : base(unit)
        {

        }
    }
}
