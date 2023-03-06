using Audit.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.Notification;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.Payment;
using NewProject.Domain.Entities.SignalR;
using NewProject.Domain.Entities.User;
using NewProject.Utility;
//using User = NewProject.Domain.Entities.User.User;

namespace NewProject.Data.Contexts
{
    public class BaseDBContext : AuditDbContext, IBaseContext
    {
        IUnitOfWork<MasterDbContext> _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;


        protected BaseDBContext()
            : base()
        {
        }

        protected BaseDBContext(DbContextOptions options)
           : base(options)
        {
        }

        protected BaseDBContext(DbContextOptions options, IUnitOfWork<MasterDbContext> context,
            IHttpContextAccessor accessor, IConfiguration configuration, IOptions<AppSettings> appSettings)
            : base(options)
        {

            _context = context;
            _accessor = accessor;
            _configuration = configuration;
            this._appSettings = appSettings.Value;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var subdomain = Constants.Shared;
                //var body = StreamToString(_accessor.HttpContext.Request.Body);
                //var jToken = JToken.Parse(body);
                if (_accessor.HttpContext.Request.Headers.ContainsKey(Constants.SubDomainNameField))
                {
                    subdomain = Convert.ToString(_accessor.HttpContext.Request.Headers[Constants.SubDomainNameField]);
                }

                if (subdomain == Constants.Shared)
                {
                    optionsBuilder = this.GetType().Name == nameof(ReadOnlyApplicationDbContext)
                       ? optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + subdomain + ":" + Constants.ReadDBConnectionName).Value)
                       : this.GetType().Name == nameof(ReadWriteApplicationDbContext)
                           ? optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + subdomain + ":" + Constants.WriteDBConnectionName).Value)
                           : optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + subdomain + ":" + Constants.AuditLogDBConnectionName).Value);
                }
                else
                {
                    if (subdomain == this._appSettings.SubDomain)
                    {
                        optionsBuilder = this.GetType().Name == nameof(ReadOnlyApplicationDbContext)
                      ? optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + this._appSettings.SubDomain + ":" + Constants.ReadDBConnectionName).Value)
                      : this.GetType().Name == nameof(ReadWriteApplicationDbContext)
                          ? optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + this._appSettings.SubDomain + ":" + Constants.WriteDBConnectionName).Value)
                          : optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + this._appSettings.SubDomain + ":" + Constants.AuditLogDBConnectionName).Value);
                        return;
                    }
                    var account = this._context.AccountsRepository.GetFirstOrDefaultAsync(c => c.Subdomain == subdomain.ToString()).Result;
                    optionsBuilder = this.GetType().Name == nameof(ReadOnlyApplicationDbContext)
                        ? optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + account.DbDomain + ":" + Constants.ReadDBConnectionName).Value)
                        : this.GetType().Name == nameof(ReadWriteApplicationDbContext)
                            ? optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + account.DbDomain + ":" + Constants.WriteDBConnectionName).Value)
                            : optionsBuilder.UseSqlServer(_configuration.GetSection("ClientConnectionStrings:" + account.DbDomain + ":" + Constants.AuditLogDBConnectionName).Value);
                }
            }
        }




        public DbSet<RefreshToken> RefreshToken { get; set; }
      
        public DbSet<UserRegister> userRegister { get; set; }

        public DbSet<UserRegisterTemp> userRegisterTemp { get; set; }
        public DbSet<MachineTable> machineTables { get; set; }
        public DbSet<CountryMaster> countryMasters { get; set; }
        public DbSet<StateMaster> stateMasters { get; set; }
        public DbSet<CityMaster> cityMasters { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Payment_Percentage> payment_Percentages { get; set; }
        public DbSet<SignalR> signalRs { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<ProviderAddress> providerAddresses { get; set; } 
    }

    public interface IBaseContext
    {
        DbSet<T> Set<T>() where T : class;

        IModel Model { get; }

        EntityEntry<T> Entry<T>(T entity) where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));


        void Dispose();

    }
}
