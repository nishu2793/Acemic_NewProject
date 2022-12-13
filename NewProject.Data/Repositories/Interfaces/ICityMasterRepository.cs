using NewProject.Data.Contexts;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories.Interfaces
{
    public interface ICityMasterRepository<TContext> : IBaseRepository<CityMaster, TContext> where TContext : IBaseContext
    {

    }
}
