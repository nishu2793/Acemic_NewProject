
namespace NewProject.Services.Entities.User
{
    public class UserProfileDto
    {
        public Guid Did { get; set; }
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? Otp { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class GetUserProfileDto
    {
        public Guid Did { get; set; }
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class UpdateUserProfileDto
    {
        public Guid Did { get; set; }
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
