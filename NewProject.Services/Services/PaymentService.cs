using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.Notification;
using NewProject.Domain.Entities.Payment;
using NewProject.Services.Entities.Notification;
using NewProject.Services.Entities.Payment;
using NewProject.Services.Interfaces;
using Newtonsoft.Json;

namespace NewProject.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public PaymentService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<List<GetPaymentDto>> GetPayment(GetPaymentDto request)
        {
            var data = (from paymentTB in _readOnlyUnitOfWork.PaymentRepository.GetAllAsQuerable()
                        where paymentTB.Did == request.Did
                        join orderTB in _readOnlyUnitOfWork.OrderRepository.GetAllAsQuerable()
                       on paymentTB.Orderid equals orderTB.OrderId

                        select new GetPaymentDto
                        {
                            Did = paymentTB.Did,
                            Orderid = orderTB.OrderId,
                            Name = paymentTB.Name,
                            EmailAddress = paymentTB.EmailAddress,
                            Paymentid = paymentTB.Paymentid,
                            Amount = orderTB.Amount,
                            Description = paymentTB.Description,
                            PhoneNumber = paymentTB.PhoneNumber,
                            Active = paymentTB.Active,
                            RequestJSON = paymentTB.RequestJSON,
                            ResponseJSON = paymentTB.ResponseJSON,
                            Paymentorderid = request.Paymentorderid,
                        }).ToList();
            return data;

        }
        public async Task<List<GetPaymentDto>> GetAllPayment()
        {
            var data = (from paymentTB in _readOnlyUnitOfWork.PaymentRepository.GetAllAsQuerable()

                        join orderTB in _readOnlyUnitOfWork.OrderRepository.GetAllAsQuerable()
                       on paymentTB.Orderid equals orderTB.OrderId

                        select new GetPaymentDto
                        {
                            Did = paymentTB.Did,
                            Orderid = orderTB.OrderId,
                            Name = paymentTB.Name,
                            EmailAddress = paymentTB.EmailAddress,
                            Paymentid = paymentTB.Paymentid,
                            Amount = orderTB.Amount,
                            Description = paymentTB.Description,
                            PhoneNumber = paymentTB.PhoneNumber,
                            Active = paymentTB.Active,
                            RequestJSON = paymentTB.RequestJSON,
                            ResponseJSON = paymentTB.ResponseJSON,
                            Paymentorderid = paymentTB.Paymentorderid,
                        }).ToList();
            return data;

        }
        public async Task<Guid> SavePayment(SavePaymentDto request)
        {
            // Update paymet status in Order 
            var data = await _readWriteUnitOfWork.OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId == request.Orderid);
            if (data != null)
            {
                data.Status = "Complete";
                await _readWriteUnitOfWork.CommitAsync();
            }
            else
            {
                data.Status = "Fail";
                await _readWriteUnitOfWork.CommitAsync();
            }

            // Save payment 
            var savePayment = new Payment()
            {
                Did = new Guid(),
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                Paymentid = request.Paymentid,
                Amount = request.Amount,
                Orderid = request.Orderid,
                Description = request.Description,
                PhoneNumber = request.PhoneNumber,
                ResponseJSON = request.ResponseJSON,
                RequestJSON = request.RequestJSON,
                Active = true,
                CreatedOn = DateTime.UtcNow,
                Paymentorderid = request.Paymentorderid,
            };
            await _readWriteUnitOfWork.PaymentRepository.AddAsync(savePayment);

            await _readWriteUnitOfWork.CommitAsync();

            // Save Notification 
            var payNotify = new PaymentNotificationDto();
            payNotify.Status = data.Status;
            payNotify.Paymentorderid = savePayment.Paymentorderid;
            payNotify.PaymentId = savePayment.Paymentid;
            payNotify.OrderId = savePayment.Orderid.ToString();
            payNotify.Amount = savePayment.Amount.ToString();
            payNotify.Email = savePayment.EmailAddress;

            var notification = new Notification()
            {
                Did = new Guid(),
                Data = JsonConvert.SerializeObject(payNotify),
                CreatedOn = DateTime.UtcNow,
                UserId=data.UserId,
                IsRead=1,
                Type = "Payment"
            };
            await _readWriteUnitOfWork.NotificationRepository.AddAsync(notification);
            await _readWriteUnitOfWork.CommitAsync();

            return savePayment.Did;
        }

    }
}
