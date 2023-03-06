using AutoMapper;
using NewProject.API.Requests.Provider;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.Provider;

namespace NewProject.API.Infrastructure.Automapper
{
    public class ProviderAddressMappingProfile : Profile
    {
        public ProviderAddressMappingProfile()
        {
            CreateMap<GetProviderAddressRequest, GetProviderAddressDto>();
            CreateMap<ProviderAddress, GetProviderAddressDto>().ReverseMap();

            CreateMap<SaveProviderAddressRequest, SaveProviderAddressDto>();
            CreateMap<ProviderAddress, SaveProviderAddressDto>().ReverseMap();

            CreateMap<UpdateProviderAddressRequest, UpdateProviderAddressDto>();
            CreateMap<ProviderAddress, UpdateProviderAddressDto>().ReverseMap();

            CreateMap<DeleteProviderAddressRequest, DeleteProviderAddressDto>();
            CreateMap<ProviderAddress, DeleteProviderAddressDto>().ReverseMap();

           // CreateMap<ProviderAuthRequest, ProviderAuthRequestDto>();
        }
    }
}
