namespace NewProject.API.Requests.SignalR
{
    public class SignalRRequest
    {
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }

    }
    public class GetSignalRRequest
    {
        public string? ConnectionId { get; set; }
    }

    public class SignalRInternalTest
    {
        public string User { get; set; }
        public string Message { get; set; }
    }

}
