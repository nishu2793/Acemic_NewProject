namespace NewProject.API.Requests.SignalR
{
    public class SignalRRequest
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
    public class GetSignalRRequest
    {
        public string? ConnectionId { get; set; }
    }
    public class SaveSignalRRequest
    {
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
}
