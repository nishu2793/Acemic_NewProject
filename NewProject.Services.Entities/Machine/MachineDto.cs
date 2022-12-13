﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Machine
{
    public class MachineDto
    {
        public Guid Did { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }   
        public string? ZipCode { get; set; }
        public decimal? Latitude { get; set; }   
        public decimal? Longitude { get; set; }  
        public string? Barcode_Number { get; set; }  
        public bool? Active { get; set; }
        public int? Admin_Id { get; set; }
        public string? Status { get; set; }
    }
}
