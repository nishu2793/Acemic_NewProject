using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;

using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility.Exceptions;
using NewProject.Utility;
using NewProject.Services.Entities.SignalR;
using NewProject.Domain.Entities.SignalR;

namespace NewProject.Services.Services
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper; 
        private readonly IFacebookService _facebookService;

        public UserRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             ReadWriteApplicationDbContext readWriteUnitOfWorkSP,IFacebookService facebookService)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
            _facebookService= facebookService;
        }

        public async Task<UserRegisterDto> UserRegisterAsync(UserRegisterDto request, string ipAddress)
        {
            var userRegister = await _readOnlyUnitOfWork.UserRegisterRepository.GetFirstOrDefaultAsync(x => x.FirstName == request.FirstName);
            var userRegisterDto = _mapper.Map<UserRegister, UserRegisterDto>(userRegister);
            return userRegisterDto;
        }

        public async Task<List<GetUserRegisterDto>> GetUserRegister(GetUserRegisterDto request)
        {
            var data = (from userRegisterTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()

                        where userRegisterTB.Did == request.Did && userRegisterTB.IsDeleted != true

                        select new GetUserRegisterDto
                        {
                            Did = userRegisterTB.Did,
                            FirstName = userRegisterTB.FirstName,
                            LastName = userRegisterTB.LastName,
                            Password = userRegisterTB.Password,
                            EmailAddress = userRegisterTB.EmailAddress,
                            MobileNo = userRegisterTB.MobileNo,
                            Gender = userRegisterTB.Gender,
                            UserToken = userRegisterTB.UserToken,
                            Otp = userRegisterTB.Otp,
                            UserType = userRegisterTB.UserType,
                        }).ToList();
            return data;
        }
        public async Task<List<GetUserRegisterDto>> GetAllUserRegister()
        {
            var data = (from userRegisterTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()

                        where userRegisterTB.IsDeleted != true
                        select new GetUserRegisterDto
                        {
                            Did = userRegisterTB.Did,
                            FirstName = userRegisterTB.FirstName,
                            LastName = userRegisterTB.LastName,
                            Password = userRegisterTB.Password,
                            EmailAddress = userRegisterTB.EmailAddress,
                            MobileNo = userRegisterTB.MobileNo,
                            Gender = userRegisterTB.Gender,
                            UserToken = userRegisterTB.UserToken,
                            Otp = userRegisterTB.Otp,
                            UserType = userRegisterTB.UserType,
                        }).ToList();
            return data;
        }
        public async Task<Guid> SaveUserRegister(SaveUserRegisterDto request)
        {
            //Filter data from UserRegisterTemp table.
            var data = await _readWriteUnitOfWork.UserRegisterTempRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);

            //Save Data in UserRegister Table.
            var saveUserRegister = new UserRegister()
            {
                Did = data.Did,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = data.Password,
                Otp = data.Otp,
                EmailAddress = request.EmailAddress,
                MobileNo = request.MobileNo,
                Gender = request.Gender,
                //Image= request.
                CreatedOn = DateTime.UtcNow,
                CreatedBy = request.Did,
                UserType=data.UserType
            };
            await _readWriteUnitOfWork.UserRegisterRepository.AddAsync(saveUserRegister);
            await _readWriteUnitOfWork.CommitAsync();

            // Delete Data from UserRegisterTemp Table.
            var datadelte = await _readWriteUnitOfWork.UserRegisterTempRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);

            if (datadelte != null)
            {
                datadelte.IsDeleted = true;
                await _readWriteUnitOfWork.CommitAsync();
            }

            // Save Data In Signal Table.
            var SaveSignalR = new SignalR()
            {
                ConnectionId = Guid.NewGuid().ToString(), // created new GUID 
                UserId = data.Did
            };
            await _readWriteUnitOfWork.SignalRRepository.AddAsync(SaveSignalR);
            await _readWriteUnitOfWork.CommitAsync();

            // Retuern Register user table GUID.
            return data.Did;
        }

        public async Task<bool> UpdateUserRegister(UpdateUserRegisterDto request)
        {
            var data = await _readWriteUnitOfWork.UserRegisterRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.FirstName = request.FirstName;
                data.LastName = request.LastName;
                data.EmailAddress = request.EmailAddress;
                data.MobileNo = request.MobileNo;
                data.Gender = request.Gender;
                data.UserToken = request.UserToken;
                data.Otp = request.Otp;
                data.UserType = request.UserType;
                data.UpdatedBy = request.Did;
                data.UpdatedOn = DateTime.UtcNow;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserRegister(DeleteUserRegisterDto request)
        {
            var data = await _readWriteUnitOfWork.UserRegisterRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.IsDeleted = true;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
      


    }
    }

