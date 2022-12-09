using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities.User
{
    [Table("UserRegister")]
    public class UserRegister
    {
        [Key]
        public Guid Did { get; set; }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Gender { get; set; }
        public string? Image { get; set; }
        public string? MobileNo { get; set; }
        public string? UserToken { get; set; }
        public string? Otp { get; set; }
        public string? RegisterType { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
