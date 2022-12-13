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

        public UnitOfWork(TContext context, IAccountsRepository<TContext> accountsRepository,
                IAdminLoginRepository<TContext> adminLoginRepository,
                IUserRegisterRepository<TContext> userRegisterRepository,
                IUserProfileRepository<TContext> userProfileRepository,
                IUserRegisterTempRepository<TContext> userRegisterTempRepository,
                IRefreshTokenRepository<TContext> refreshTokenRepository,
                 IMachineRepository<TContext> machineRepository
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

