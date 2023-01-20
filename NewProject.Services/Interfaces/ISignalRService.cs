using NewProject.Services.Entities.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Interfaces
{
    public interface ISignalRService
    {
        Task<List<GetSignalRDto>> GetSignalR(GetSignalRDto getSignalRDto);
        Task<List<GetSignalRDto>> GetAllSignalR();

        Task<List<SaveSignalDto>> SaveSignalR(SaveSignalDto saveSignalDto);
    }
}
