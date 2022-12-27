using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.SignalR;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;

        public SignalRService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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

     
        public async Task<List<GetSignalRDto>> GetSignalR(GetSignalRDto request)
        {
            var data = (from signalRTB in _readOnlyUnitOfWork.SignalRRepository.GetAllAsQuerable()
                        where signalRTB.ConnectionId == request.ConnectionId
                        join user in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                        on signalRTB.UserId equals user.Did

                        select new GetSignalRDto
                        {
                            ConnectionId= signalRTB.ConnectionId,
                            UserId= user.Did,
                            
                        }).ToList();
            return data;
        }
        public async Task<List<GetSignalRDto>> GetAllSignalR()
        {
            var data = (from signalRTB in _readOnlyUnitOfWork.SignalRRepository.GetAllAsQuerable()
                        join user in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                        on signalRTB.UserId equals user.Did
                        select new GetSignalRDto
                        {
                            ConnectionId = signalRTB.ConnectionId,
                            UserId = user.Did,
                        }).ToList();
            return data;
        }

    }
}
