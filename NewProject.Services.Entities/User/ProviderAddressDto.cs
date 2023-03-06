using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Provider
{
    public class ProviderAddressDto
    {
        public Guid AddressId { get; set; }
        public Guid ProviderRegisterid { get; set; }

        public DateTime? CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

    }

    public class GetProviderAddressDto
    {
        public Guid AddressId { get; set; }
        public Guid ProviderRegisterid { get; set; }

        public DateTime? CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class SaveProviderAddressDto
    {
        public Guid AddressId { get; set; }
        public Guid ProviderRegisterid { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class UpdateProviderAddressDto
    {
        public Guid AddressId { get; set; }
        public Guid ProviderRegisterid { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public int ZipCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class DeleteProviderAddressDto
    {
        public Guid? AddressId { get; set; }
    }
}
