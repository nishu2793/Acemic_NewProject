using NewProject.Services.Entities.Notification;
using NewProject.Services.Entities.Payment;

namespace NewProject.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<GetPaymentDto>> GetPayment(GetPaymentDto request);
        Task<List<GetPaymentDto>> GetAllPayment();
        Task<PaymentNotificationDto> SavePayment(SavePaymentDto request);
        Task<bool> UpdatePayment(UpdatePaymentDto request);
    }
}
