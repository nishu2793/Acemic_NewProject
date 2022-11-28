using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;
using NewProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateSecurityToken(SessionDetailsDto sessionDetailsDto, AppSettings appSettings, out DateTime expiresOn);
        public SessionDetailsDto ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
