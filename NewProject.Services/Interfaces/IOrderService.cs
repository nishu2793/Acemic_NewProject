using NewProject.Services.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<GetOrderDto>> GetOrder(GetOrderDto request);
        Task<List<GetOrderDto>> GetAllOrder();
        Task<Guid> SaveOrder(SaveOrderDto request);
        Task<bool> UpdateOrder(UpdateOrderDto request);
        Task<bool> DeleteOrder(DeleteOrderDto request);
    }
}
