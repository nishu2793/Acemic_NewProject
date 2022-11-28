using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminLoginService _adminLoginService;

        public AdminLoginController(IAdminLoginService adminLoginService , IMapper mapper)
        {
            _mapper = mapper;
            _adminLoginService = adminLoginService;

        }
        //[HttpPost("getuser")]
        //public async Task<Dictionary<string, object>> UserAsync([FromBody] UserRequest request)
        //{
        //    var userDto = _mapper.Map<UserRequest, UserDto>(request);
        //    var result = await _userService.UserAsync(userDto, null);
        //    return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };

        //}

        [HttpPost("GetAdminLogin")]
        public async Task<Dictionary<string, object>> GetAdminLogin([FromBody] GetAdminLoginRequest request)
        {
            var adminLogindto = _mapper.Map<GetAdminLoginRequest, GetAdminLoginDto>(request);
            var result = await _adminLoginService.GetAdminLogin(adminLogindto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };

        }

        [HttpPost("GetAllAdminLogin")]
        public async Task<Dictionary<string, object>> GetAllAdminLogin()
        {
            var result = await _adminLoginService.GetAllAdminLogin();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };

        }

        [HttpPost("SaveAdminLogin")]
        public async Task<Dictionary<string, object>> SaveAdminLogin([FromBody] SaveAdminLoginRequest request)
        {
            var saveadminLoginDto = _mapper.Map<SaveAdminLoginRequest, SaveAdminLoginDto>(request);
            var result = await _adminLoginService.SaveAdminLogin(saveadminLoginDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };

        }
        //[HttpPost("UpdateUser")]
        //public async Task<Dictionary<string, object>> UpdateUser([FromBody] UpdateUserRequest request)
        //{
        //    var updateUser = _mapper.Map<UpdateUserRequest, updateUserDto>(request);
        //    var result = await _userService.UpdateUser(updateUser);
        //    return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        //}

        //[HttpPost("DeleteUser")]
        //public async Task<Dictionary<string, object>> DeleteUser([FromBody] DeleteUserRequest request)
        //{
        //    var deleteUser = _mapper.Map<DeleteUserRequest, DeleteUserDto>(request);
        //    var result = await _userService.DeleteUser(deleteUser);
        //    return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        //}

    }
}
