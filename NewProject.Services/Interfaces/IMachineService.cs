using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.Provider;
using NewProject.Services.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface  IMachineService
    {
        Task<List<MachineDto>> GetMachine(MachineDto request);
        Task<List<MachineDto>> GetAllMachine();
        Task<List<MachineDto>> SaveMachine(MachineDto request);
        Task<bool> UpdateMachine(MachineDto request);
        Task<bool> DeleteMachine(DeleteMachineDto request);

    }
}
