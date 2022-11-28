using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Utility
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string SubDomain { get; set; }
        public string JwtExpiryInMinutes { get; set; }
        public int RefreshTokenTTL { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
        public string FacebookRedirectUri { get; set; }
    }
}
