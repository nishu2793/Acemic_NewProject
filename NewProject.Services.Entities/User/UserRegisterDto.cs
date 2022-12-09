
namespace NewProject.Services.Entities.User
{
    public class UserRegisterDto
    {
        public Guid Did { get; set; }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? UserToken { get; set; }
        public string? Otp { get; set; }
        public string? RegisterType { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class GetUserRegisterDto
    {
        public Guid Did { get; set; }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Otp { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? UserToken { get; set; }
        // public string? Otp { get; set; }
        public string? RegisterType { get; set; }
        public string? Password { get; set; }
    }
    public class SaveUserRegisterDto
    {
        public Guid Did { get; set; }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? UserToken { get; set; }
        public string? Otp { get; set; }
        public string? RegisterType { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
    public class UpdateUserRegisterDto
    {
        public Guid Did { get; set; }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? UserToken { get; set; }
        public string? Otp { get; set; }
        public string? RegisterType { get; set; }
        public string? Password { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
    public class DeleteUserRegisterDto
    {
        public Guid Did { get; set; }
    }

}

