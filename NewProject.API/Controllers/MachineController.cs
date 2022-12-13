using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.Machine;
using NewProject.API.Requests.User;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Services.Services;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMachineService  _machineService;
        private readonly IConfiguration _configuration;
        public MachineController(IMachineService machineService, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _machineService = machineService;
            _configuration = configuration;
        }

        [HttpPost("GetMachinebyId")]
        public async Task<Dictionary<string, object>> GetMachinebyId([FromBody] GetMachineRequest request)
        {
            var machinedto = _mapper.Map<GetMachineRequest, MachineDto>(request);
            var result = await _machineService.GetMachine(machinedto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetAllMachine")]
        public async Task<Dictionary<string, object>> GetAllMachine()
        {
            var result = await _machineService.GetAllMachine();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
