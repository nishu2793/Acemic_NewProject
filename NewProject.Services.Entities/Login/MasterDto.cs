﻿namespace NewProject.Services.Entities.LoginDto
{
   
    public class CountryMasterDto
    {
        public int ID { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }


    }

    public class StateMasterDto
    {
        public int ID { get; set; }
        public string? StateName { get; set; }
        public string? CountryCode { get; set; }
    }
    public class CityMasterDto
    {
        public int ID { get; set; }
        public string? CityName { get; set; }
        public int? StateId { get; set; }
    }
}
