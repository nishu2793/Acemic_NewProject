using AutoMapper;
using MailKit.Net.Smtp;
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

                        where userRegisterTempTB.Did == request.Did || userRegisterTempTB.EmailAddress == request.EmailAddress
                       
                        select new GetUserRegisterTempDto
                        {
                            Did = userRegisterTempTB.Did,

                            FirstName = userRegisterTempTB.FirstName,
                            LastName = userRegisterTempTB.LastName,
                            Password=userRegisterTempTB.Password,


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
                            Password = userRegisterTB.Password,


                            EmailAddress = userRegisterTB.EmailAddress,
                            MobileNo = userRegisterTB.MobileNo,




                        }).ToList();

            return data;
        }
        
        public async Task<Guid> SaveUserRegisterTemp(SaveUserRegisterTempDto request)
        {
            Random randomObj = new Random();
            string Otp = randomObj.Next(1000, 9999).ToString();
            var saveUserRegisterTemp = new UserRegisterTemp()
            {
                Did = new Guid(),

                FirstName = request.FirstName,
                LastName = request.LastName,
               Otp=Otp,
                Password=request.Password,
                EmailAddress = request.EmailAddress,
                MobileNo = request.MobileNo,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,

            };
            SendUserWelcomeEmail(saveUserRegisterTemp.FirstName, saveUserRegisterTemp.EmailAddress, saveUserRegisterTemp.Otp);
            await _readWriteUnitOfWork.UserRegisterTempRepository.AddAsync(saveUserRegisterTemp);
            await _readWriteUnitOfWork.CommitAsync();
            //var id = saveUserRegisterTemp.Did;
            //var data = (from userRegisterTempTB in _readOnlyUnitOfWork.UserRegisterTempRepository.GetAllAsQuerable()

            //            where userRegisterTempTB.Did == id
            //            select new SaveUserRegisterTempDto
            //            {
            //                Did = userRegisterTempTB.Did,
            //                EmailAddress = userRegisterTempTB.EmailAddress,
            //                Otp = userRegisterTempTB.Otp,
            //            }).ToList();


        
            return saveUserRegisterTemp.Did;
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

        public bool SendUserWelcomeEmail(string UserName, string UserEmail, string Otp)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress("Acemic", "rizvan.dcs@gmail.com");
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(UserName, UserEmail);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = "Welcome To Acemic";

                string FilePath = Directory.GetCurrentDirectory() + "\\MailHtml\\WelcomeEmail.html";
                string EmailTemplateText = File.ReadAllText(FilePath);

                EmailTemplateText = string.Format(EmailTemplateText, UserName, Otp, DateTime.Now.Date.ToShortDateString());
                EmailTemplateText = EmailTemplateText.Replace("{0}", UserName);
                EmailTemplateText = EmailTemplateText.Replace("{1}", Otp.ToString());

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = EmailTemplateText;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                //emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                //emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
                emailClient.Connect("smtp.gmail.com", 465, true);
                emailClient.Authenticate("rizvan.dcs@gmail.com", "mbmnouohsqikztst");
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }

    }
}