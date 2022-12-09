using AutoMapper;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<GetUserProfileRequest, GetUserProfileDto>();
            CreateMap<UserRegister, GetUserProfileDto>().ReverseMap();

            CreateMap<UpdateUserProfileRequest, UpdateUserProfileDto>();
            CreateMap<UserRegister, UpdateUserProfileDto>().ReverseMap();
        }
    }
}
