namespace NewProject.API.Requests.Notification
{
    public class NotificationRequest
    {
        public Guid Did { get; set; }

        public string? Data { get; set; }

        public Guid? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? IsRead { get; set; }

        public DateTime? ReadOn { get; set; }
        public string? Type { get; set; }
    }
    public class GetNotificationRequest
    {
        public Guid? UserId { get; set; }
        public string? Type { get; set; }
    }
}

