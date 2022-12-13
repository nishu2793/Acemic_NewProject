
namespace NewProject.Domain.Entities.User
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string EmailFrom { get; set; }
        public int Port { get; set; }
    }
}
