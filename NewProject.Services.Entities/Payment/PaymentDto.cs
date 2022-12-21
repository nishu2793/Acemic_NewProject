using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Payment
{
    public class PaymentDto
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


    }
    public class GetPaymentDto
    {
        public Guid Did { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Paymentid { get; set; }
        public decimal? Amount { get; set; }
        public Guid Orderid { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? ResponseJSON { get; set; }
        public string? RequestJSON { get; set; }
        public string? Paymentorderid { get; set; }

    }

    public class SavePaymentDto
    {
        public Guid Did { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Paymentid { get; set; }
        public decimal? Amount { get; set; }
        public Guid Orderid { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? ResponseJSON { get; set; }
        public string? RequestJSON { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Paymentorderid { get; set; }

    }
    public class UpdatePaymentDto
    {
        public Guid Did { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Paymentid { get; set; }
        public decimal? Amount { get; set; }
        public Guid Orderid { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? ResponseJSON { get; set; }
        public string? RequestJSON { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Paymentorderid { get; set; }

    }
    public class DeletePaymentDto
    {
        public Guid Did { get; set; }
    }


}
