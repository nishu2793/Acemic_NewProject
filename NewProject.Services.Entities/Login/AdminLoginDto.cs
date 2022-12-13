namespace NewProject.Services.Entities.LoginDto
{
    public class AdminLoginDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class GetAdminLoginDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
    }
    public class SaveAdminLoginDto
    {
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class CountryMasterDto
    {
        public int ID { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }


    }

    public class StateMasterDto
    {
        public int ID { get; set; }
        public string? StateName { get; set; }
        public string? CountryCode { get; set; }
    }
    public class CityMasterDto
    {
        public int ID { get; set; }
        public string? CityName { get; set; }
        public int? StateId { get; set; }
    }
}
