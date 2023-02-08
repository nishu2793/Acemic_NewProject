using NewProject.Services.Entities.Order;
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
        Task<List<Payment_PercentageDto>> SavePayment(SavePaymentDto request);
        Task<bool> UpdatePayment(UpdatePaymentDto request);
    }
}
