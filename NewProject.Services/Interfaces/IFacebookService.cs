﻿using NewProject.Services.Entities.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IFacebookService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        //Task<FaceBookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
