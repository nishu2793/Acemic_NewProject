using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.Notification
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public Guid Did { get; set; }

        public string? Data { get; set; }

        public Guid? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? IsRead { get; set; }

        public DateTime? ReadOn { get; set; }
        public string? Type { get; set; }

    }
}
