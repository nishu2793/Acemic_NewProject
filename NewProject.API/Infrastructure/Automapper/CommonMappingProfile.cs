using AutoMapper;
using NewProject.API.Requests.Common;
using NewProject.Services.Entities.Common;

namespace NewProject.API.Infrastructure.Automapper
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap<PaginationRequest, PaginationDto>();
        }
    }
}

