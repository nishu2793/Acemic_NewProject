
using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;

namespace NewProject.Services.Services
{
    public class UserRegisterTempService : IUserRegisterTempService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public UserRegisterTempService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<List<GetUserRegisterTempDto>> GetUserRegisterTemp(GetUserRegisterTempDto request)
        {
            var data = (from userRegisterTempTB in _readOnlyUnitOfWork.UserRegisterTempRepository.GetAllAsQuerable()
                        where userRegisterTempTB.Did == request.Did || userRegisterTempTB.EmailAddress == request.EmailAddress
                        select new GetUserRegisterTempDto
                        {
                            Did = userRegisterTempTB.Did,
                            FirstName = userRegisterTempTB.FirstName,
                            LastName = userRegisterTempTB.LastName,
                            Password = userRegisterTempTB.Password,
                            EmailAddress = userRegisterTempTB.EmailAddress,
                            MobileNo = userRegisterTempTB.MobileNo,
                            UserType=userRegisterTempTB.UserType,
                        }).ToList();
            return data;
        }
        public async Task<List<GetUserRegisterTempDto>> GetAllUserRegisterTemp()
        {
            var data = (from userRegisterTB in _readOnlyUnitOfWork.UserRegisterTempRepository.GetAllAsQuerable()
                        where userRegisterTB.IsDeleted != true
                        select new GetUserRegisterTempDto
                        {
                            Did = userRegisterTB.Did,
                            FirstName = userRegisterTB.FirstName,
                            LastName = userRegisterTB.LastName,
                            Password = userRegisterTB.Password,
                            EmailAddress = userRegisterTB.EmailAddress,
                            MobileNo = userRegisterTB.MobileNo,
                            UserType=userRegisterTB.UserType,
                        }).ToList();
            return data;
        }
        public async Task<List<SaveUserRegisterTempDto>> SaveUserRegisterTemp(SaveUserRegisterTempDto request, MailSettings Mailsettingdata)
        {
            Random randomObj = new Random();
            string Otp = randomObj.Next(1000, 9999).ToString();
            Guid Id = new Guid();
            var saveUserRegisterTemp = new UserRegisterTemp()
            {
                Did = Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Otp = Otp,
                Password = request.Password,
                EmailAddress = request.EmailAddress,
                UserType= request.UserType,
                MobileNo = request.MobileNo,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = Id,
            };
            await _readWriteUnitOfWork.UserRegisterTempRepository.AddAsync(saveUserRegisterTemp);
            await _readWriteUnitOfWork.CommitAsync();
            var data = (from userRegisterTempTB in _readOnlyUnitOfWork.UserRegisterTempRepository.GetAllAsQuerable()
                        where userRegisterTempTB.Did == saveUserRegisterTemp.Did
                        select new SaveUserRegisterTempDto
                        {
                            Did = userRegisterTempTB.Did,
                            EmailAddress = userRegisterTempTB.EmailAddress,
                            Otp = userRegisterTempTB.Otp,
                            UserType = userRegisterTempTB.UserType,
                        }).ToList();
            if (saveUserRegisterTemp.EmailAddress != null)
            {
                try
                {
                    MailService mailService = new MailService();
                    mailService.SendEmail(saveUserRegisterTemp, Mailsettingdata);
                }
                catch (Exception ex)
                { }
            }
            else
            {

            }
            return data;
        }
        public async Task<bool> UpdateUserRegisterTemp(UpdateUserRegisterTempDto request)
        {
            var data = await _readWriteUnitOfWork.UserRegisterTempRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.FirstName = request.FirstName;
                data.LastName = request.LastName;
                data.EmailAddress = request.EmailAddress;
                data.Password = request.Password;
                data.MobileNo = request.MobileNo;
                data.UpdatedBy = request.Did;
                data.UpdatedOn = DateTime.UtcNow;
                data.UserType= request.UserType;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUserRegisterTemp(DeleteUserRegisterTempDto request)
        {
            var data = await _readWriteUnitOfWork.UserRegisterTempRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.IsDeleted = true;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<List<VerifyotpDto>> Verifyotp(VerifyotpDto request)
        {
            var data = (from userRegisterTempTB in _readOnlyUnitOfWork.UserRegisterTempRepository.GetAllAsQuerable()
                        where userRegisterTempTB.Otp == request.Otp && (userRegisterTempTB.EmailAddress == request.EmailAddress || userRegisterTempTB.MobileNo == request.MobileNo)
                        select new VerifyotpDto
                        {
                            Did = userRegisterTempTB.Did,
                            EmailAddress = userRegisterTempTB.EmailAddress,
                            MobileNo = userRegisterTempTB.MobileNo,
                            Otp = userRegisterTempTB.Otp,
                        }).ToList();
            return data;
        }
        public async Task<Guid> SavePasswordTemp(SavePasswordTempDto request)
        {
            var data = await _readWriteUnitOfWork.UserRegisterTempRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);
            if (data != null)
            {
                data.Password = GenericMethods.GetHash(request.Password);
                await _readWriteUnitOfWork.CommitAsync();
                return data.Did;
            }
            return Guid.Empty;
        }
    }
}