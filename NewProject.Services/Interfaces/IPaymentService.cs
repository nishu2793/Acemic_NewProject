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
        Task<Guid> SavePayment(SavePaymentDto request);
        Task<bool> UpdatePayment(UpdatePaymentDto request);
        Task<bool> DeletePaymnet(DeletePaymentDto request);
    }
}
