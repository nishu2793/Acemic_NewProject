using AutoMapper;
using MailKit.Search;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;

namespace NewProject.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<List<GetOrderDto>> GetOrder(GetOrderDto request)
        {
            var data = (from orderTB in _readOnlyUnitOfWork.OrderRepository.GetAllAsQuerable()
                        where orderTB.OrderId == request.OrderId
                        join machineTB in _readOnlyUnitOfWork.MachineRepository.GetAllAsQuerable()
                       on orderTB.MachineId equals machineTB.Did
                        join userTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                       on orderTB.UserId equals userTB.Did
                        select new GetOrderDto
                        {
                            OrderId = orderTB.OrderId,
                            MachineId = machineTB.Did,
                            UserId = userTB.Did,
                            Amount = orderTB.Amount,
                            Active = orderTB.Active,
                            OrderType= orderTB.OrderType,   
                            Status= orderTB.Status,
                            StatusMessage= orderTB.StatusMessage

                        }).ToList();
            return data;

        }
        public async Task<List<GetOrderDto>> GetAllOrder()
        {
            var data = (from orderTB in _readOnlyUnitOfWork.OrderRepository.GetAllAsQuerable()
                        join machineTB in _readOnlyUnitOfWork.MachineRepository.GetAllAsQuerable()
                       on orderTB.MachineId equals machineTB.Did
                        join userTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                       on orderTB.UserId equals userTB.Did
                        select new GetOrderDto
                        {
                            OrderId = orderTB.OrderId,
                            MachineId = machineTB.Did,
                            UserId = userTB.Did,
                            Amount = orderTB.Amount,
                            Active = orderTB.Active,
                            OrderType = orderTB.OrderType,
                            Status = orderTB.Status,
                            StatusMessage = orderTB.StatusMessage

                        }).ToList();
            return data;

        }
        public async Task<List<SaveOrderDto>> SaveOrder(SaveOrderDto request)
        {
            Guid Id = Guid.NewGuid();
            var saveOrder = new Order()
            {

                OrderId= Id,
                MachineId = request.MachineId,
                UserId = request.UserId,
                Amount = request.Amount,
                Active = true,
                OrderType = request.OrderType,
                Status = "Draft",
                StatusMessage = "Null",
                CreatedOn = DateTime.UtcNow,
                
            };
            await _readWriteUnitOfWork.OrderRepository.AddAsync(saveOrder);
            await _readWriteUnitOfWork.CommitAsync();
            var data = (
                from orderTB in _readOnlyUnitOfWork.OrderRepository.GetAllAsQuerable()
                where orderTB.OrderId == saveOrder.OrderId
                join userTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                        on orderTB.UserId equals userTB.Did
                        select new SaveOrderDto
                        {
                            OrderId = orderTB.OrderId,
                            Amount= orderTB.Amount,
                            EmailAddress = userTB.EmailAddress,
                            Firstname=userTB.FirstName,
                            Lastname=userTB.LastName
                           
                        }).ToList();
            return data;
        }
        public async Task<bool> UpdateOrder(UpdateOrderDto request)
        {
            var data = await _readWriteUnitOfWork.OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId == request.OrderId);

            if (data != null)
            {
                data.MachineId = request.MachineId;
                data.UserId = request.UserId;
                data.Amount = request.Amount;
                data.Active = request.Active;
                data.OrderType = request.OrderType;
                data.Status = request.Status;
                data.StatusMessage = request.StatusMessage;
                data.UpdatedOn = DateTime.UtcNow;

                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateStatus(UpdateStatusDto request)
        {
            Guid dataorder = new Guid(request.OrderId);
            var data = await _readWriteUnitOfWork.OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId == dataorder);

            if (data != null)
            {
                data.Status = "InProgress";
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteOrder(DeleteOrderDto request)
        {
            var data = await _readWriteUnitOfWork.OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId == request.OrderId);
            if (data != null)
            {
                data.Active = true;
                await _readWriteUnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
    }
}
