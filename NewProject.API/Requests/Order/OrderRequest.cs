namespace NewProject.API.Requests.Order
{
    public class OrderRequest
    {
        public Guid OrderId { get; set; }
        public Guid? MachineId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }

    public class GetOrderRequest
    {
        public Guid OrderId { get; set; }

    }

    public class SaveOrderRequest
    {
        public Guid? MachineId { get; set; }
        public Guid? UserId { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }

    public class UpdateOrderRequest
    {
        public Guid OrderId { get; set; }
        public Guid? MachineId { get; set; }
        public Guid? UserId { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }

        public string StatusMessage { get; set; }

    }
    public class DeleteOrderRequest
    {
        public Guid OrderId { get; set; }

    }
}

