namespace NewProject.API.Requests.User
{
    public class UserRegisterTempRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class GetUserRegisterTempRequest
    {
        public Guid? Did { get; set; }
        public string? EmailAddress { get; set;}

    }
    public class Verifyotp
    {
        public Guid Did { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }

        public string? Otp { get; set; }
    }

public class SaveUserRegisterTempRequest
    {
      
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }
      public string? Password { get; set; }
        public string? MobileNo { get; set; }

      
  

    }


   

    public class UpdateUserRegisterTempRequest
    {
        public Guid Did { get; set; }
       
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }

        public string? Password { get; set; }
        public string? MobileNo { get; set; }
       



       

    }


    public class SavePasswordTempRequest
    {
        public Guid Did { get; set; }
        public string? Password { get; set; }


    }
        public class DeleteUserRegisterTempRequest
    {
        public Guid Did { get; set; }

    }

}

