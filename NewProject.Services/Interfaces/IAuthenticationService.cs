using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress);
    }
}
