using NewProject.Domain.Entities.Notification;
using NewProject.Services.Entities.Notification;
using NewProject.Services.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<GetPaymentDto>> GetPayment(GetPaymentDto request);
        Task<List<GetPaymentDto>> GetAllPayment();
        Task<PaymentNotificationDto> SavePayment(SavePaymentDto request);
       

        }
}
