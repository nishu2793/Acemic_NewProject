using AutoMapper;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.Payment;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.Payment;
using NewProject.Services.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var saveOrder = new Payment()
            {
                Did = new Guid(),
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
            await _readWriteUnitOfWork.PaymentRepository.AddAsync(saveOrder);

            await _readWriteUnitOfWork.CommitAsync();

            return saveOrder.Did;
        }
        public async Task<bool> UpdatePayment(UpdatePaymentDto request)
        {

            var data = await _readWriteUnitOfWork.PaymentRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);

            if (data != null)
            {
                data.Did = request.Did;
                data.EmailAddress = request.EmailAddress;
                data.Paymentid = request.Paymentid;
                data.Amount = request.Amount;
                data.Orderid = request.Orderid;
                data.Description = request.Description;
                data.PhoneNumber = request.PhoneNumber;
                data.ResponseJSON = request.ResponseJSON;
                data.RequestJSON = request.RequestJSON;
                data.Active = true;
                data.UpdatedOn = DateTime.UtcNow;
                data.Paymentorderid = request.Paymentorderid;
                await _readWriteUnitOfWork.CommitAsync();

                return true;

            }
            return false;

        }
        public async Task<bool> DeletePaymnet(DeletePaymentDto request)
        {

            var data = await _readWriteUnitOfWork.PaymentRepository.GetFirstOrDefaultAsync(x => x.Did == request.Did);

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
