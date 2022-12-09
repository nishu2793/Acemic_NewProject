using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.LoginDto;
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

        public AdminLoginController(IAdminLoginService adminLoginService, IMapper mapper)
        {
            _mapper = mapper;
            _adminLoginService = adminLoginService;

        }
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
    }
}
