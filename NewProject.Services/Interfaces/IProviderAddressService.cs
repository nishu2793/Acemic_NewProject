using NewProject.Services.Entities.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface IProviderAddressService
    {
        Task<List<GetProviderAddressDto>> GetProviderAddress(GetProviderAddressDto request);
        Task<List<GetProviderAddressDto>> GetAllProviderAddress();
        Task<List<SaveProviderAddressDto>> SaveProviderAddress(SaveProviderAddressDto request);
        Task<bool> UpdateProviderAddress(UpdateProviderAddressDto request);
        Task<bool> DeleteProviderAddress(DeleteProviderAddressDto request);
    }
}
