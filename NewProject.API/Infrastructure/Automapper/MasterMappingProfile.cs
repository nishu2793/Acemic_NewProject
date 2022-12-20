using AutoMapper;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{
    public class MasterMappingProfile : Profile
    {
        public MasterMappingProfile()
        {
           

            CreateMap<CityMasterRequest, CityMasterDto>();
            CreateMap<CityMaster, CityMasterDto>().ReverseMap();

            CreateMap<StateMasterRequest, StateMasterDto>();
            CreateMap<StateMaster, StateMasterDto>().ReverseMap();


        }
    }
}
