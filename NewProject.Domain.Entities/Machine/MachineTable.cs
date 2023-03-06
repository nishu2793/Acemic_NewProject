using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.Machine
{
    [Table("Machine")]
    public class MachineTable
    {
        [Key]
        public Guid Did { get; set; }
        public Guid? ProviderId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public decimal?  Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? BarcodeNumber { get; set; }
        public bool? Active { get; set; }
        public string? Status { get; set; }
        public bool? IsDeleted { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
