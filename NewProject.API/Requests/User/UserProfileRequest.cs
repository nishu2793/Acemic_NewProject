namespace NewProject.API.Requests.User
{
    public class UserProfileRequest
    {

        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string?    LastName { get; set; }

        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
    }
    public class GetUserProfileRequest
    {
        public Guid Did { get; set; }

    }

    public class UpdateUserProfileRequest
    {
        public Guid Did { get; set; }
        public IFormFile? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }

    }
}
