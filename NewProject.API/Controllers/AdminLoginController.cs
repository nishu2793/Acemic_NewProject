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

        [HttpPost("GetCountry")]
        public async Task<Dictionary<string, object>> GetCountry()
        {
            var result = await _adminLoginService.GetCountry();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetState")]
        public async Task<Dictionary<string, object>> GetState([FromBody] StateMasterRequest request)
        {
            var statedto = _mapper.Map<StateMasterRequest, StateMasterDto>(request);
            var result = await _adminLoginService.GetState(statedto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetCity")]
        public async Task<Dictionary<string, object>> GetCity([FromBody] CityMasterRequest request)
        {
            var citydto = _mapper.Map<CityMasterRequest, CityMasterDto>(request);
            var result = await _adminLoginService.GetCity(citydto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
