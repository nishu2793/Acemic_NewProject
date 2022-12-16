using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.Order
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid? Machine_Id { get; set; }
        public Guid? User_Id { get; set;}
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Amount { get; set; }
        public bool? Active { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
      
        public string StatusMessage { get; set; }
    }
}
