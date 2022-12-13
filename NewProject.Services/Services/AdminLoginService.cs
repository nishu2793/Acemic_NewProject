using AutoMapper;
using MailKit.Search;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;

namespace NewProject.Services.Services
{
    public class AdminLoginService : IAdminLoginService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public AdminLoginService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             ReadWriteApplicationDbContext readWriteUnitOfWorkSP)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
        }

        public async Task<AdminLoginDto> AdminLoginAsync(AdminLoginDto request, string ipAddress)
        {
            var adminlogin = await _readOnlyUnitOfWork.AdminLoginRepository.GetFirstOrDefaultAsync(x => x.UserName == request.UserName);
            var adminLoginDto = _mapper.Map<AdminLogin, AdminLoginDto>(adminlogin);
            return adminLoginDto;
        }

        public async Task<List<GetAdminLoginDto>> GetAdminLogin(GetAdminLoginDto request)
        {
            var data = (from adminloginTB in _readOnlyUnitOfWork.AdminLoginRepository.GetAllAsQuerable()
                        where adminloginTB.Id == request.Id && adminloginTB.IsDeleted != true
                        select new GetAdminLoginDto
                        {
                            Id = adminloginTB.Id,
                            UserName = adminloginTB.UserName,
                            EmailAddress = adminloginTB.EmailAddress,
                            Password = adminloginTB.Password,
                        }).ToList();

            return data;
        }

        public async Task<List<GetAdminLoginDto>> GetAllAdminLogin()
        {
            var data = (from adminloginTB in _readOnlyUnitOfWork.AdminLoginRepository.GetAllAsQuerable()

                        where adminloginTB.IsDeleted != true
                        select new GetAdminLoginDto
                        {
                            Id = adminloginTB.Id,
                            UserName = adminloginTB.UserName,
                            EmailAddress = adminloginTB.EmailAddress,
                            Password = adminloginTB.Password,
                        }).ToList();
            return data;
        }

        public async Task<bool> SaveAdminLogin(SaveAdminLoginDto request)
        {
            var saveAdminLogin = new AdminLogin()
            {
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                Password = GenericMethods.GetHash(request.Password),
                CreatedOn = DateTime.UtcNow,
            };
            await _readWriteUnitOfWork.AdminLoginRepository.AddAsync(saveAdminLogin);
            await _readWriteUnitOfWork.CommitAsync();
            return true;
        }
        public async Task<List<CountryMasterDto>> GetCountry()
        {
            var data = (from countryMasterTB in _readOnlyUnitOfWork.CountryMasterRepository.GetAllAsQuerable()

                       

                        select new CountryMasterDto
                        {
                            ID = countryMasterTB.ID,
                            CountryName = countryMasterTB.CountryName,

                            CountryCode = countryMasterTB.CountryCode


                        }).ToList();
            return data;


        }
        public async Task<List<StateMasterDto>> GetState(StateMasterDto request)
        {
            var data = (from stateMasterTB in _readOnlyUnitOfWork.StateMasterRepository.GetAllAsQuerable()
                        where stateMasterTB.CountryCode == request.CountryCode
                        join countryMasterTB in _readOnlyUnitOfWork.CountryMasterRepository.GetAllAsQuerable()
                       on stateMasterTB.CountryCode equals countryMasterTB.CountryCode
                        select new StateMasterDto
                        {
                            ID = stateMasterTB.ID,
                            StateName = stateMasterTB.StateName,
                            CountryCode= countryMasterTB.CountryCode

                        }).ToList();
            return data;


        }
        public async Task<List<CityMasterDto>> GetCity(CityMasterDto request)
        {
            var data = (from cityMasterTB in _readOnlyUnitOfWork.CityMasterRepository.GetAllAsQuerable()
                        where cityMasterTB.StateId == request.StateId
                        join stateMasterTB in _readOnlyUnitOfWork.StateMasterRepository.GetAllAsQuerable()
                     on cityMasterTB.StateId equals stateMasterTB.ID

                        select new CityMasterDto
                        {
                            ID = cityMasterTB.ID,
                            CityName = cityMasterTB.CityName,
                            StateId= stateMasterTB.ID

                         


                        }).ToList();
            return data;


        }
    }
}
