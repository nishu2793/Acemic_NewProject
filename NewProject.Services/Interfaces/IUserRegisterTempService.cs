﻿using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;
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
        Task<List<VerifyotpDto>> Verifyotp(VerifyotpDto request);
        Task<List<GetUserRegisterTempDto>> GetAllUserRegisterTemp();
        Task<List<SaveUserRegisterTempDto>> SaveUserRegisterTemp(SaveUserRegisterTempDto request, MailSettings Mailsettingdata);
        Task<bool> UpdateUserRegisterTemp(UpdateUserRegisterTempDto request);
        Task<Guid> SavePasswordTemp(SavePasswordTempDto request);
        Task<bool> DeleteUserRegisterTemp(DeleteUserRegisterTempDto request);
    }
}
