using AutoMapper;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{
    public class AdminLoginMappingProfile : Profile
    {
        public  AdminLoginMappingProfile()
        {
            CreateMap<GetAdminLoginRequest, GetAdminLoginDto>();
            CreateMap<AdminLogin, GetAdminLoginDto>().ReverseMap();

            CreateMap<SaveAdminLoginRequest, SaveAdminLoginDto>();
            CreateMap<AdminLogin, SaveAdminLoginDto>().ReverseMap();
          
           // CreateMap<AdminLoginRequest, AdminLoginDto>();

        }
    }
}
