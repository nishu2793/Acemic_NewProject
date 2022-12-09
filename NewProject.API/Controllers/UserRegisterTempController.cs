using AceMic.Domain.Entities.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterTempController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRegisterTempService _userRegisterTempService;
        private readonly IConfiguration _configuration;
        public UserRegisterTempController(IUserRegisterTempService userRegisterTempService, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRegisterTempService = userRegisterTempService;
            _configuration = configuration;
        }
        [HttpPost("GetUserRegisterTemp")]
        public async Task<Dictionary<string, object>> GetUserRegisterTemp([FromBody] GetUserRegisterTempRequest request)
        {
            var userdto = _mapper.Map<GetUserRegisterTempRequest, GetUserRegisterTempDto>(request);
            var result = await _userRegisterTempService.GetUserRegisterTemp(userdto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };

        }
        [HttpPost("GetAllUserRegisterTemp")]
        public async Task<Dictionary<string, object>> GetAllUserRegisterTemp()
        {
            var result = await _userRegisterTempService.GetAllUserRegisterTemp();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("SaveUserRegisterTemp")]
        public async Task<Dictionary<string, object>> SaveUserRegisterTemp([FromBody] SaveUserRegisterTempRequest request)
        {
            var Mailsettingdata = _configuration.GetSection("MailSettings").Get<MailSettings>();
            var saveUserRegisterDto = _mapper.Map<SaveUserRegisterTempRequest, SaveUserRegisterTempDto>(request);
            var result = await _userRegisterTempService.SaveUserRegisterTemp(saveUserRegisterDto, Mailsettingdata);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("Verifyotp")]
        public async Task<Dictionary<string, object>> Verifyotp([FromBody] Verifyotp request)
        {
            var userdto = _mapper.Map<Verifyotp, VerifyotpDto>(request);
            var result = await _userRegisterTempService.Verifyotp(userdto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("SavePasswordTemp")]
        public async Task<Dictionary<string, object>> SavePasswordTemp([FromBody] SavePasswordTempRequest request)
        {
            var savepasswordDto = _mapper.Map<SavePasswordTempRequest, SavePasswordTempDto>(request);
            var result = await _userRegisterTempService.SavePasswordTemp(savepasswordDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("UpdateUserRegisterTemp")]
        public async Task<Dictionary<string, object>> UpdateUserRegisterTemp([FromBody] UpdateUserRegisterTempRequest request)
        {
            var updateUserRegisterDto = _mapper.Map<UpdateUserRegisterTempRequest, UpdateUserRegisterTempDto>(request);
            var result = await _userRegisterTempService.UpdateUserRegisterTemp(updateUserRegisterDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("DeleteUserRegisterTemp")]
        public async Task<Dictionary<string, object>> DeleteUserRegisterTemp([FromBody] DeleteUserRegisterTempRequest request)
        {
            var deleteUserRegister = _mapper.Map<DeleteUserRegisterTempRequest, DeleteUserRegisterTempDto>(request);
            var result = await _userRegisterTempService.DeleteUserRegisterTemp(deleteUserRegister);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
