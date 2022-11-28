using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Id = new int(),
                UserName = request.UserName,


                EmailAddress = request.EmailAddress,
              
                Password = GenericMethods.GetHash(request.Password),
              
                CreatedOn = DateTime.UtcNow,
               


            };
            await _readWriteUnitOfWork.AdminLoginRepository.AddAsync(saveAdminLogin);

            await _readWriteUnitOfWork.CommitAsync();

            return true;
        }
    }
}
