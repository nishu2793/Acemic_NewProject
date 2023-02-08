namespace NewProject.API.Requests.Payment
{
    public class PaymentRequest
    {
        public Guid Did { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Paymentid { get; set; }
        public decimal? Amount { get; set; }
        public Guid Orderid { get; set; }
        public string? Description { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public string? ResponseJSON { get; set; }
        public string? RequestJSON { get; set; }
        public string? Paymentorderid { get; set; }
        public string? Transfer_Details { get; set; }

    }
    public class GetPaymentRequest
    {
        public Guid Did { get; set;}
    }
    public class SavePaymentRequest
    {

        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Paymentid { get; set; }
        public decimal? Amount { get; set; }
        public Guid? Orderid { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? ResponseJSON { get; set; }
        public string? RequestJSON { get; set; }
        public string? Paymentorderid { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Transfer_Details { get; set; }

    }


    public class UpdatePaymentRequest
    {
        public string? Paymentid { get; set; }
        public string? Transfer_Details { get; set; }
    }
}
