using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.SignalR
{
    [Table("SignalR")]
    public class SignalR
    {
        [Key]
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
}
