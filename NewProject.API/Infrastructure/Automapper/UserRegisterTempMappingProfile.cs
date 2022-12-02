using AutoMapper;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{ 
    public class UserRegisterTempMappingProfile: Profile
    {
        public UserRegisterTempMappingProfile()
        {
            CreateMap<GetUserRegisterTempRequest, GetUserRegisterTempDto>();
            CreateMap<UserRegisterTemp, GetUserRegisterTempDto>().ReverseMap();

            CreateMap<Verifyotp, VerifyotpDto>();
            CreateMap<UserRegisterTemp, VerifyotpDto>().ReverseMap();

            CreateMap<SaveUserRegisterTempRequest, SaveUserRegisterTempDto>();
            CreateMap<UserRegisterTemp, SaveUserRegisterTempDto>().ReverseMap();

            CreateMap<UpdateUserRegisterTempRequest, UpdateUserRegisterTempDto>();
            CreateMap<UserRegisterTemp, UpdateUserRegisterTempDto>().ReverseMap();

            CreateMap<DeleteUserRegisterTempRequest, DeleteUserRegisterTempDto>();
            CreateMap<UserRegisterTemp, DeleteUserRegisterTempDto>().ReverseMap();

            
        }
    }
}
