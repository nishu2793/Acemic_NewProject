using AutoMapper;
using NewProject.API.Requests.SignalR;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.SignalR;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.SignalR;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{
    public class SignalRMappingProfile : Profile
    {
        public SignalRMappingProfile()
        {
            CreateMap<GetSignalRRequest, GetSignalRDto>();
            CreateMap<SignalR, GetSignalRDto>().ReverseMap();

        }
    }

    }
