using AutoMapper;
using MailKit.Search;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.Order;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.Order;
using NewProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                       on orderTB.Machine_Id equals machineTB.Did
                        join userTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                       on orderTB.User_Id equals userTB.Did
                        select new GetOrderDto
                        {
                            OrderId = orderTB.OrderId,
                            Machine_Id = machineTB.Did,
                            User_Id = userTB.Did,
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
                       on orderTB.Machine_Id equals machineTB.Did
                        join userTB in _readOnlyUnitOfWork.UserRegisterRepository.GetAllAsQuerable()
                       on orderTB.User_Id equals userTB.Did
                        select new GetOrderDto
                        {
                            OrderId = orderTB.OrderId,
                            Machine_Id = machineTB.Did,
                            User_Id = userTB.Did,
                            Amount = orderTB.Amount,
                            Active = orderTB.Active,
                            OrderType = orderTB.OrderType,
                            Status = orderTB.Status,
                            StatusMessage = orderTB.StatusMessage

                        }).ToList();
            return data;

        }
        public async Task<bool> SaveOrder(SaveOrderDto request)
        {
            var saveOrder = new Order()
            {
                OrderId= new Guid(),
                Machine_Id = request.Machine_Id,
                User_Id = request.User_Id,
                Amount = request.Amount,
                Active = request.Active,
                OrderType = request.OrderType,
                Status = request.Status,
                StatusMessage = request.StatusMessage,
                CreatedOn = DateTime.UtcNow,



            };
            await _readWriteUnitOfWork.OrderRepository.AddAsync(saveOrder);

            await _readWriteUnitOfWork.CommitAsync();

            return true;
        }
        public async Task<bool> UpdateOrder(UpdateOrderDto request)
        {

            var data = await _readWriteUnitOfWork.OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId == request.OrderId);

            if (data != null)
            {
                data.Machine_Id = request.Machine_Id;
                data.User_Id = request.User_Id;
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
