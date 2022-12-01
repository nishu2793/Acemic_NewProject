﻿using NewProject.Services.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IUserRegisterTempService
    {
        Task<List<GetUserRegisterTempDto>> GetUserRegisterTemp(GetUserRegisterTempDto request);
        Task<List<GetUserRegisterTempDto>> GetAllUserRegisterTemp();
        Task<Guid> SaveUserRegisterTemp(SaveUserRegisterTempDto request);
        Task<bool> UpdateUserRegisterTemp(UpdateUserRegisterTempDto request);
        Task<bool> DeleteUserRegisterTemp(DeleteUserRegisterTempDto request);
    }
}