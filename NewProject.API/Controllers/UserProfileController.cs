using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualBasic;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Services.Services;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
      


        public UserProfileController(IUserProfileService userProfileService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _userProfileService = userProfileService;
           

        }

        [HttpPost("GetUserProfile")]
        public async Task<Dictionary<string, object>> GetUserProfile([FromBody] GetUserProfileRequest request)
        {
            var userprofiledto = _mapper.Map<GetUserProfileRequest, GetUserProfileDto>(request);
            var result = await _userProfileService.GetUserProfile(userprofiledto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };

        }
        [HttpPost("UpdateUserProfile")]
        public async Task<Dictionary<string, object>> UpdateUserProfile([FromBody] UpdateUserProfileRequest request)
        {
            var updateUserProfileDto = _mapper.Map<UpdateUserProfileRequest, UpdateUserProfileDto>(request);

            var result = await _userProfileService.UpdateUserProfile(updateUserProfileDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }


    }
}

