using MimeKit.Cryptography;
using NewProject.API.Infrastructure.Models;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using PushNotification.Models;

namespace NewProject.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress);
        Task<string> MQTT(string Message);

    }
}
