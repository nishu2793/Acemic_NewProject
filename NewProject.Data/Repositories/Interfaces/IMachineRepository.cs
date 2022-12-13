using NewProject.Data.Contexts;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories.Interfaces
{
    public interface IMachineRepository<TContext> : IBaseRepository<MachineTable, TContext> where TContext : IBaseContext
    {


    }
}
