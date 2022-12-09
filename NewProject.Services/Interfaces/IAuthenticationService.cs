using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;

namespace NewProject.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress);
    }
}
