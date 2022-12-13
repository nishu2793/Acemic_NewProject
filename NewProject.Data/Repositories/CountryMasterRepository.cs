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
    public class CountryMasterRepository<TContext> : BaseRepository<CountryMaster, TContext>, ICountryMasterRepository<TContext> where TContext : IBaseContext
    {
        public CountryMasterRepository(TContext unit) : base(unit)
        {

        }
    }
}
