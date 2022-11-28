using NewProject.Services.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<List<GetUserProfileDto>> GetUserProfile(GetUserProfileDto request);
        Task<bool> UpdateUserProfile(UpdateUserProfileDto request);
    }
}
