namespace NewProject.API.Requests
{
    public class CreateNewUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public int MobileNo { get; set; }
        public string Password { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
