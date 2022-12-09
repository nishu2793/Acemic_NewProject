using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NewProject.Domain.Entities.Master;
using NewProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Data.Contexts
{
    [AuditDbContext(AuditEventType = "{database}")]
    public class MasterDbContext : AuditDbContext, IBaseContext
    {
        internal MasterDbContext(DbContextOptions options)
         : base(options)
        {
        }

        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<UserRegister> UserRegister { get; set; }




    }
}
