using AutoMapper;
using MimeKit;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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

                        where userRegisterTempTB.Did == request.Did && userRegisterTempTB.IsDeleted != true
                        select new GetUserRegisterTempDto
                        {
                            Did = userRegisterTempTB.Did,
                        
                            FirstName = userRegisterTempTB.FirstName,
                            LastName = userRegisterTempTB.LastName,
                          

                            EmailAddress = userRegisterTempTB.EmailAddress,
                            MobileNo = userRegisterTempTB.MobileNo,




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
                            
                            EmailAddress = userRegisterTB.EmailAddress,
                            MobileNo = userRegisterTB.MobileNo,

                       


                        }).ToList();

            return data;
        }

        public async Task<List<SaveUserRegisterTempDto>> SaveUserRegisterTemp(SaveUserRegisterTempDto request)
        {
            Random randomObj = new Random();
            string Otp = randomObj.Next(1000, 9999).ToString();
            var saveUserRegisterTemp = new UserRegisterTemp()
            {
                Did = new Guid(),
           
                FirstName = request.FirstName,
                LastName = request.LastName,
                Otp= Otp,
                EmailAddress = request.EmailAddress,
                MobileNo = request.MobileNo,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,

            };
            await _readWriteUnitOfWork.UserRegisterTempRepository.AddAsync(saveUserRegisterTemp);
            await _readWriteUnitOfWork.CommitAsync();
            var id = saveUserRegisterTemp.Did;
            var data = (from userRegisterTempTB in _readOnlyUnitOfWork.UserRegisterTempRepository.GetAllAsQuerable()

                        where userRegisterTempTB.Did == id
                        select new SaveUserRegisterTempDto
                        {
                            Did = userRegisterTempTB.Did,
                            EmailAddress = userRegisterTempTB.EmailAddress,
                            Otp = userRegisterTempTB.Otp,
                        }).ToList();

            
            //if(saveUserRegisterTemp.MobileNo != null)
            //{
            //  using(MailMessage mm=new MailMessage())

            //}
            if (saveUserRegisterTemp.EmailAddress != null)
            {

                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("dishas.dcs@gmail.com", "Dcs@12345");

                //MailAddress to = new MailAddress("rizvan.dcs@gmail.com");
                //MailAddress from = new MailAddress("ashishp.dcs@gmail.com");
                //MailMessage message = new MailMessage(from, to);
                //message.Subject = "Good morning";
                //message.Body = "Charles, Harry, There are a few unpaid invoices for the Royal Wedding. Let's talk this over on Monday.;";

                //try
                //{
                //    smtp.Send(message);
                //}
                //catch (SmtpException ex)
                //{
                //    Console.WriteLine(ex.ToString());
                //}

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
                data.MobileNo = request.MobileNo;
              

                data.UpdatedBy = 1;
                data.UpdatedOn = DateTime.UtcNow;

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

                        where userRegisterTempTB.Otp == request.Otp && userRegisterTempTB.EmailAddress == request.EmailAddress || userRegisterTempTB.MobileNo == request.MobileNo
                        select new VerifyotpDto
                        {
                            Did = userRegisterTempTB.Did,
                            EmailAddress = userRegisterTempTB.EmailAddress,
                            MobileNo = userRegisterTempTB.MobileNo,
                            Otp = userRegisterTempTB.Otp,
                        }).ToList();

            return data;
        }

    }
}
