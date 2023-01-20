using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.Payment
{
    [Table("Payment_Percentage")]
    public class Payment_Percentage
    {
        [Key]
        public int Id { get; set; }
        public double? Percentage { get; set; }
        public string? Name { get; set; }
        public string? AccountId { get; set; }
    }
}
