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
        Task<List<GetSignalRDto>> GetSignalR(GetSignalRDto request);
        Task<List<GetSignalRDto>> GetAllSignalR();
    }
}
