using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.SignalR;
using NewProject.Services.Entities.SignalR;
using NewProject.Services.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;

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

        public async Task<List<SaveSignalDto>> SaveSignalR(SaveSignalDto saveSignalDto)
        {
            var findRecord = await  _readWriteUnitOfWork.SignalRRepository.GetFirstOrDefaultAsync(x => x.UserId == saveSignalDto.UserId);
            if(findRecord != null)
            {
                findRecord.ConnectionId = saveSignalDto.ConnectionId;
                await _readWriteUnitOfWork.CommitAsync();
            }
            else
            {
                var SaveSignalRdata = new SignalR()
                {
                    ConnectionId = saveSignalDto.ConnectionId,
                    UserId = saveSignalDto.UserId,
                };
                await _readWriteUnitOfWork.SignalRRepository.AddAsync(SaveSignalRdata);
                await _readWriteUnitOfWork.CommitAsync();
            }
            var data = (from SignalRTB in _readOnlyUnitOfWork.SignalRRepository.GetAllAsQuerable()
                        where SignalRTB.ConnectionId == saveSignalDto.ConnectionId
                        select new SaveSignalDto
                        {
                            Id = SignalRTB.Id,
                            ConnectionId = SignalRTB.ConnectionId,
                            UserId = SignalRTB.UserId
                        }).ToList();
            return data;
        }
        public async Task<string> Connection(Guid orderId)
        {

            var connetionid = (from signalRTB in _readOnlyUnitOfWork.SignalRRepository.GetAllAsQuerable()

                               join user in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                               on signalRTB.UserId equals user.Did
                               join order in _readOnlyUnitOfWork.OrderRepository.GetAllAsQuerable()
                               on user.Did equals order.UserId
                               // orderID
                               where order.OrderId == orderId
                               select new GetSignalRDto
                               {
                                   ConnectionId = signalRTB.ConnectionId,
                                   // UserId = order.UserId,

                               });
            // return  connetionid.ToList();   
            
            if(connetionid != null) { 

            foreach(var item in connetionid)
            {
              return  item.ConnectionId.ToString();
            
            }

            }
            return "Empty";
            
        }
    }

    }

