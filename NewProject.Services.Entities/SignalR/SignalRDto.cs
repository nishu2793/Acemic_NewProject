using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.SignalR
{
    public class SignalRDto
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
    public class GetSignalRDto
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
        public Guid OrderId { get; set; }

    }
    public class SaveSignalDto
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
    public class UpdateSignalDto
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public Guid? UserId { get; set; }
    }
}