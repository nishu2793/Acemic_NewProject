using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.SignalR
{
    public class SignalRDto
    {
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
    public class GetSignalRDto
    {
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
}
