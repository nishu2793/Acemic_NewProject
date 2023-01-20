using Microsoft.EntityFrameworkCore;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Data.Repositories;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using System.Reflection;
using NetCore.AutoRegisterDi;
using CorePush.Apple;
using CorePush.Google;

namespace NewProject.API.Infrastructure.Extensions
{
    public  static class IServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            var assembliesToScan = new[]{
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(IBaseService))
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                 .Where(c => c.Name.EndsWith("Service"))
                 .AsPublicImplementedInterfaces();
        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IAccountsRepository<>), typeof(AccountsRepository<>));      
            services.AddTransient(typeof(IUserRegisterRepository<>), typeof(UserRegisterRepository<>));
            services.AddTransient(typeof(IUserRegisterTempRepository<>), typeof(UserRegisterTempRepository<>));
            services.AddTransient(typeof(IRefreshTokenRepository<>), typeof(RefreshTokenRepository<>));
            services.AddTransient(typeof(IUserProfileRepository<>), typeof(UserProfileRepository<>));
            services.AddTransient(typeof(IMachineRepository<>), typeof(MachineRepository<>));
            services.AddTransient(typeof(ICountryMasterRepository<>), typeof(CountryMasterRepository<>));
            services.AddTransient(typeof(IStateMasterRepository<>), typeof(StateMasterRepository<>));
            services.AddTransient(typeof(ICityMasterRepository<>), typeof(CityMasterRepository<>));
            services.AddTransient(typeof(IOrderRepository<>), typeof(OrderRepository<>));
            services.AddTransient(typeof(IPaymentRepository<>), typeof(PaymentRepository<>));
            services.AddTransient(typeof(ISignalRRepository<>), typeof(SignalRRepository<>));
            services.AddTransient(typeof(INotificationRepository<>), typeof(NotificationRepository<>));
            services.AddTransient(typeof(IPayment_PercentageRepository<>), typeof(Payment_PercentageRepository<>));
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
        }

        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(Constants.MasterDBConnectionName));
            });

            //DependencyResolver.Current.GetService<IAuthProviderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddDbContext<AuditLogDbContext>(options => { });

            services.AddDbContext<ReadWriteApplicationDbContext>(options => { });
            services.AddDbContext<ReadOnlyApplicationDbContext>(options => { });

            services.AddScoped<Func<ReadOnlyApplicationDbContext>>((provider) => () => provider.GetService<ReadOnlyApplicationDbContext>());
            services.AddScoped<Func<ReadWriteApplicationDbContext>>((provider) => () => provider.GetService<ReadWriteApplicationDbContext>());
            services.AddScoped<Func<AuditLogDbContext>>((provider) => () => provider.GetService<AuditLogDbContext>());
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            //var urls = settings.ClientAppUrl.Split(",", StringSplitOptions.RemoveEmptyEntries);

            services.AddCors(options =>
            {
                options.AddPolicy("NewProjectCors", b =>
                {
                    b.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:4000")
                    .SetIsOriginAllowed((host) => true);
                });
            });
        }
    }
}
