using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.User
{

    [Table("AdminLogin")]
    public class AdminLogin
    {
        [Key]
         public int Id { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }

        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }



    }
}
