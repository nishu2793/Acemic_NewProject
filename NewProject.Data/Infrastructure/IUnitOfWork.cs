using NewProject.Data.Contexts;
using NewProject.Data.Repositories.Interfaces;

namespace NewProject.Data.Infrastructure
{
    public interface IUnitOfWork<TContext> where TContext : IBaseContext
    {
        IAccountsRepository<TContext> AccountsRepository { get; }
        IAdminLoginRepository<TContext> AdminLoginRepository { get; }
        IUserRegisterRepository<TContext> UserRegisterRepository { get; }
        IRefreshTokenRepository<TContext> RefreshTokenRepository { get; }
        IUserProfileRepository<TContext> UserProfileRepository { get; }
        IUserRegisterTempRepository<TContext> UserRegisterTempRepository { get; }
        IMachineRepository<TContext> MachineRepository { get; }
        ICityMasterRepository<TContext> CityMasterRepository { get; }
        ICountryMasterRepository<TContext> CountryMasterRepository { get; }
        IStateMasterRepository<TContext> StateMasterRepository { get; }
        IOrderRepository<TContext> OrderRepository { get; }
        Task<int> CommitAsync();

    }
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : IBaseContext
    {
        public TContext Context { get; }
        public IAccountsRepository<TContext> AccountsRepository { get; }
        public IAdminLoginRepository<TContext> AdminLoginRepository { get; }
        public IUserRegisterRepository<TContext> UserRegisterRepository { get; }
        public IUserProfileRepository<TContext> UserProfileRepository { get; }
        public IUserRegisterTempRepository<TContext> UserRegisterTempRepository { get; }
        public IRefreshTokenRepository<TContext> RefreshTokenRepository { get; }

        public IMachineRepository<TContext> MachineRepository { get; }
       public ICityMasterRepository<TContext> CityMasterRepository { get; }
       public ICountryMasterRepository<TContext> CountryMasterRepository { get; }
       public  IStateMasterRepository<TContext> StateMasterRepository { get; }
        public IOrderRepository<TContext> OrderRepository { get; }

        public UnitOfWork(TContext context, IAccountsRepository<TContext> accountsRepository,
                IAdminLoginRepository<TContext> adminLoginRepository,
                IUserRegisterRepository<TContext> userRegisterRepository,
                IUserProfileRepository<TContext> userProfileRepository,
                IUserRegisterTempRepository<TContext> userRegisterTempRepository,
                IRefreshTokenRepository<TContext> refreshTokenRepository,
                 IMachineRepository<TContext> machineRepository,
                  ICityMasterRepository<TContext> cityMasterRepository,
                  IStateMasterRepository<TContext> stateMasterRepository,
                  ICountryMasterRepository<TContext>countryMasterRepository,
                  IOrderRepository<TContext> orderRepository
                )
        {
            this.Context = context;
            this.AccountsRepository = accountsRepository;
            this.AdminLoginRepository = adminLoginRepository;
            this.UserRegisterRepository = userRegisterRepository;
            this.RefreshTokenRepository = refreshTokenRepository;
            this.UserProfileRepository = userProfileRepository;
            this.UserRegisterTempRepository = userRegisterTempRepository;
            this.MachineRepository= machineRepository;
            this.CountryMasterRepository = countryMasterRepository;
            this.StateMasterRepository = stateMasterRepository;
            this.CityMasterRepository = cityMasterRepository;
            this.OrderRepository= orderRepository;


        }
        public async Task<int> CommitAsync()
        {
            TContext checkType = default(TContext);
            if (checkType is ReadOnlyApplicationDbContext)
            {
                throw new Exception("This is a read-only context!");
            }
            return await this.Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}

