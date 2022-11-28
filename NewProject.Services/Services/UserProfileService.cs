using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Data.Repositories.Interfaces;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace NewProject.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
       
      




        public UserProfileService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<List<GetUserProfileDto>> GetUserProfile(GetUserProfileDto request)
        {

            var data = (from userProfileTB in _readOnlyUnitOfWork.UserProfileRepository.GetAllAsQuerable()  
                        where userProfileTB.Did == request.Did
                        select new GetUserProfileDto
                        {
                            Did = userProfileTB.Did,

                            FirstName = userProfileTB.FirstName,
                            LastName = userProfileTB.LastName,


                            EmailAddress = userProfileTB.EmailAddress,
                            MobileNo = userProfileTB.MobileNo,

                            Gender = userProfileTB.Gender,
                            Image = userProfileTB.Image,




                        }).ToList();




            return data;
           
            

            
          
        }
        public async Task<bool> UpdateUserProfile(UpdateUserProfileDto request)
        {

            var data = await _readWriteUnitOfWork.UserProfileRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);

            if (data != null)
            {

                data.FirstName = request.FirstName;
                data.LastName = request.LastName;

                data.EmailAddress = request.EmailAddress;
                data.MobileNo = request.MobileNo;
                data.Gender = request.Gender;
                data.Image = request.Image;
            
              
                
                    

                    data.UpdatedBy = 1;
                data.UpdatedOn = DateTime.UtcNow;

                await _readWriteUnitOfWork.CommitAsync();
                
               
                return true;

              
                    
                    }

            return false;


        }

        

        }



    }



