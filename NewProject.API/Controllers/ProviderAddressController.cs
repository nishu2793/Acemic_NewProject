using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Requests.Provider;
using NewProject.Services.Entities.Provider;
using NewProject.Services.Interfaces;
using NewProject.Utility;

namespace NewProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderAddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProviderAddressService _ProviderAddressService;
        public ProviderAddressController(IProviderAddressService ProviderAddressService, IMapper mapper)
        {
            _mapper = mapper;
            _ProviderAddressService = ProviderAddressService;
        }

        [HttpPost("GetProviderAddress")]
        public async Task<Dictionary<string, object>> GetProviderAddress([FromBody] GetProviderAddressRequest request)
        {
            var Providerdto = _mapper.Map<GetProviderAddressRequest, GetProviderAddressDto>(request);
            var result = await _ProviderAddressService.GetProviderAddress(Providerdto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("GetAllProviderAddress")]
        public async Task<Dictionary<string, object>> GetAllProviderAddress()
        {
            var result = await _ProviderAddressService.GetAllProviderAddress();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("SaveProviderAddress")]
        public async Task<Dictionary<string, object>> SaveProviderAddress([FromBody] SaveProviderAddressRequest request)
        {
            var saveProviderAddressDto = _mapper.Map<SaveProviderAddressRequest, SaveProviderAddressDto>(request);
            var result = await _ProviderAddressService.SaveProviderAddress(saveProviderAddressDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("UpdateProviderAddress")]
        public async Task<Dictionary<string, object>> UpdateProviderAddress([FromBody] UpdateProviderAddressRequest request)
        {
            var updateProviderAddressDto = _mapper.Map<UpdateProviderAddressRequest, UpdateProviderAddressDto>(request);
            var result = await _ProviderAddressService.UpdateProviderAddress(updateProviderAddressDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("DeleteProviderAddress")]
        public async Task<Dictionary<string, object>> DeleteProviderAddress([FromBody] DeleteProviderAddressRequest request)
        {
            var deleteProviderAddress = _mapper.Map<DeleteProviderAddressRequest, DeleteProviderAddressDto>(request);
            var result = await _ProviderAddressService.DeleteProviderAddress(deleteProviderAddress);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

    }
}