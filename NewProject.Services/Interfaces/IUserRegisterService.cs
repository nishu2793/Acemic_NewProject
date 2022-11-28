using NewProject.Services.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IUserRegisterService
    {
        Task<UserRegisterDto> UserRegisterAsync(UserRegisterDto request, string ipAddress);
        Task<List<GetUserRegisterDto>> GetUserRegister(GetUserRegisterDto request);
        Task<List<GetUserRegisterDto>> GetAllUserRegister();
        Task<bool> SaveUserRegister(SaveUserRegisterDto request);   
        Task<bool> UpdateUserRegister(UpdateUserRegisterDto request);
        Task<bool> DeleteUserRegister(DeleteUserRegisterDto request);


    }
}
