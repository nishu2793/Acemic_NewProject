﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Services.Entities.Common
{
    public class PaginationDto
    {
        public string? GlobalSearch { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
    }
}
