﻿using System.ComponentModel.DataAnnotations;

namespace NewProject.Services.Entities.Master
{
    public class AccountDto
    {
        [Key]
        public int Id { get; set; }
        public Guid DId { get; set; }
        public string Name { get; set; }
        public string Subdomain { get; set; }
        public string DbDomain { get; set; }
        public int LicenseCount { get; set; }
        public string ProductType { get; set; }
        public bool IsEXCEL { get; set; }
        public string Status { get; set; }
        public string SetupQRCodeUrl { get; set; }
    }
}
