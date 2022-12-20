using NewProject.Services.Entities.LoginDto;

namespace NewProject.Services.Interfaces
{
    public interface IMasterService
    {
      
        Task<List<CountryMasterDto>> GetCountry();
        Task<List<StateMasterDto>> GetState(StateMasterDto request);
        Task<List<CityMasterDto>> GetCity(CityMasterDto request);

    }
}