using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid MachineId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }

    public class GetOrderDto
    {
        public Guid OrderId { get; set; }
        public Guid MachineId { get; set; }
        public Guid UserId { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }
    public class SaveOrderDto
    {
        public Guid OrderId { get; set; }
        public Guid MachineId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }

    }
    public class UpdateOrderDto
    {
        public Guid OrderId { get; set; }
        public Guid MachineId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }
    public class DeleteOrderDto
    {
        public Guid OrderId { get; set; }
    }

}
