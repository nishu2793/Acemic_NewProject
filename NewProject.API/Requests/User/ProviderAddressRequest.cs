using NewProject.Domain.Entities.User;

namespace NewProject.API.Requests.Provider
{
    public class ProviderAddressRequest
    {   
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; } 
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class GetProviderAddressRequest
    {
        public Guid AddressId { get; set; }
    }
    public class SaveProviderAddressRequest
    { 
        public Guid ProviderRegisterid { get; set; }  
        public string? Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
    }
    public class UpdateProviderAddressRequest
    {
        public Guid AddressId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class DeleteProviderAddressRequest
    {
        public Guid AddressId { get; set; }
    }
}
