using AutoMapper;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{
    public class UserRegisterMappingProfile : Profile
    {
        public  UserRegisterMappingProfile()
        {
            CreateMap<GetUserRegisterRequest, GetUserRegisterDto>();
            CreateMap<UserRegister, GetUserRegisterDto>().ReverseMap();

            CreateMap<SaveUserRegisterRequest, SaveUserRegisterDto>();
            CreateMap<UserRegister, SaveUserRegisterDto>().ReverseMap();

            CreateMap<UpdateUserRegisterRequest, UpdateUserRegisterDto>();
            CreateMap<UserRegister, UpdateUserRegisterDto>().ReverseMap();

            CreateMap<DeleteUserRegisterRequest, DeleteUserRegisterDto>();
            CreateMap<UserRegister, DeleteUserRegisterDto>().ReverseMap();

            CreateMap<UserAuthRequest, UserAuthRequestDto>();
        }
    }
}

