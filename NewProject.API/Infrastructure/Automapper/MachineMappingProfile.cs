using AutoMapper;
using NewProject.API.Requests.Machine;
using NewProject.API.Requests.User;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.User;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.User;

namespace NewProject.API.Infrastructure.Automapper
{
    public class MachineMappingProfile : Profile
    {
        public MachineMappingProfile()
        {
            CreateMap<GetMachineRequest, MachineDto>();
            CreateMap<MachineTable, MachineDto>().ReverseMap();

            CreateMap<SaveMachineRequest, MachineDto>();
            CreateMap<MachineTable, MachineDto>().ReverseMap();

            CreateMap<UpdateMachineRequest, MachineDto>();
            CreateMap<MachineTable, MachineDto>().ReverseMap();

            CreateMap<DeleteMachineRequest, DeleteMachineDto>();
            CreateMap<MachineTable, DeleteMachineDto>().ReverseMap();
        }
    }
}
