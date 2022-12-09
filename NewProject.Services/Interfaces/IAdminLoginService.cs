using NewProject.Services.Entities.LoginDto;

namespace NewProject.Services.Interfaces
{
    public interface IAdminLoginService
    {
        Task<AdminLoginDto> AdminLoginAsync(AdminLoginDto request, string ipAddress);
        Task<List<GetAdminLoginDto>> GetAdminLogin(GetAdminLoginDto request);
        Task<List<GetAdminLoginDto>> GetAllAdminLogin();
        Task<bool> SaveAdminLogin(SaveAdminLoginDto request);
    }
}