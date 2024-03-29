﻿
namespace NewProject.Services.Entities.User
{
    public class UserRegisterTempDto
    {
        public Guid Did { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Otp { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class GetUserRegisterTempDto
    {
        public Guid Did { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? UserType { get; set; }
    }

    public class SaveUserRegisterTempDto
    {
        public Guid Did { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? UserType { get; set; }
        public string? Otp { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
    public class UpdateUserRegisterTempDto
    {
        public Guid Did { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Password { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UserType { get; set; }
    }
    public class DeleteUserRegisterTempDto
    {
        public Guid Did { get; set; }
    }
    public class VerifyotpDto
    {
        public Guid Did { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Otp { get; set; }
    }
    public class SavePasswordTempDto
    {
        public Guid Did { get; set; }
        public string? Password { get; set; }

    }


}