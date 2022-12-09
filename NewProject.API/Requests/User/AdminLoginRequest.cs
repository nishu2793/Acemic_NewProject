namespace NewProject.API.Requests.User
{
    public class AdminLoginRequest
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }

        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class GetAdminLoginRequest
    {
        public int Id { get; set; }

    }
    public class SaveAdminLoginRequest
    {
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
