using AutoMapper;
using NewProject.API.Requests.SignalR;
using NewProject.Domain.Entities.SignalR;
using NewProject.Services.Entities.SignalR;

namespace NewProject.API.Infrastructure.Automapper
{
    public class SignalRMappingProfile : Profile
    {
        public SignalRMappingProfile()
        {
            CreateMap<GetSignalRRequest, GetSignalRDto>();
            CreateMap<SignalR, GetSignalRDto>().ReverseMap();
            CreateMap<SaveSignalRRequest, SaveSignalDto>();
            CreateMap<SignalR, SaveSignalDto>().ReverseMap();
        }
    }
}
