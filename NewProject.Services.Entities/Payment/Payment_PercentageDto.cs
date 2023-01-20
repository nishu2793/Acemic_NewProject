using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Payment
{
    public class Payment_PercentageDto
    {
        public int Id { get; set; }
        public double? Percentage { get; set; }
        public string? Name { get; set; }
        public string? AccountId { get; set; }
    }
}
