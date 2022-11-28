using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.LoginDto
{
    public class UserLoginDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? Isd { get; set; }
        public string? MobileNo { get; set; }
        public int NotificationType { get; set; }
        public bool IsPwdResetRequired { get; set; }
        public string? JwtToken { get; set; }
        public int CurrentConnectionId { get; set; }
        public int ThemeId { get; set; }
        public string? LanguageCode { get; set; }

        //[JsonIgnore]
        public string? RefreshToken { get; set; }

        public string? InvitationToken { get; set; }
    }
}
