using NewProject.Services.Entities.LoginDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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