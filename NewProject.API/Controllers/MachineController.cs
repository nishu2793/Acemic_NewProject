using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.Machine;
using NewProject.API.Requests.Provider;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.Provider;
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
        [HttpPost("SaveMachine")]
        public async Task<Dictionary<string, object>> SaveMachine([FromBody] SaveMachineRequest request)
        {
            var SaveMachineDto = _mapper.Map<SaveMachineRequest, MachineDto>(request);
            var result = await _machineService.SaveMachine(SaveMachineDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("UpdateMachine")]
        public async Task<Dictionary<string, object>> UpdateMachine([FromBody] UpdateMachineRequest request)
        {
            var UpdateMachineDto = _mapper.Map<UpdateMachineRequest, MachineDto>(request);
            var result = await _machineService.UpdateMachine(UpdateMachineDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("DeleteMachine")]
        public async Task<Dictionary<string, object>> DeleteMachine([FromBody] DeleteMachineRequest request)
        {
            var deleteMachine = _mapper.Map<DeleteMachineRequest, DeleteMachineDto>(request);
            var result = await _machineService.DeleteMachine(deleteMachine);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
