using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Repositories
{
    public class MachineRepository<TContext> : BaseRepository<MachineTable, TContext>, IMachineRepository<TContext> where TContext : IBaseContext
    {
        public MachineRepository(TContext unit) : base(unit)
        {

        }

    }
}
