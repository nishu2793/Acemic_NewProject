using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Notification
{
    public class NotificationDto
    {
        public Guid Did { get; set; }

        public string? Data { get; set; }

        public Guid? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? IsRead { get; set; }

        public DateTime? ReadOn { get; set; }
        public string? Type { get; set; }
    }
    public class GetNotificationRequestDto
    {
        public Guid? UserId { get; set; }
        public string? Type { get; set; }
    }

    public class GetNotificationDto
    {
        public Guid Did { get; set; }

        public string? Data { get; set; }

        public Guid? UserId { get; set; }
        public int? IsRead { get; set; }
        public DateTime? ReadOn { get; set; }
        public string? Type { get; set; }
    }

    public class PaymentNotificationDto
    {
        public string Status { get; set; }
        public string? OrderId { get; set; }
        public string? PaymentId { get; set; }

        public string? Amount { get; set; }
        public string? Email { get; set; }
        public string? Paymentorderid { get; set; }
    }
}
