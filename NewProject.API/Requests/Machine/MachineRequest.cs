namespace NewProject.API.Requests.Machine
{
    public class MachineRequest
    {
        public Guid Did { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string BarcodeNumber { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class GetMachineRequest
    {
        public string BarcodeNumber { get; set; }
    }
}
