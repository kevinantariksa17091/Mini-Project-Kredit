using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Project_Kredit.Models
{
    public class VillageDto
    {
        public string VillageId { get; set; } = null!;
        public string VillageName { get; set; } = null!;
        public string DistrictId { get; set; } = null!;
        public string DistrictName { get; set; } = null!;
        public string RegencyId { get; set; } = null!;
        public string RegencyName { get; set; } = null!;
        public string ProvinceId { get; set; } = null!;
        public string ProvinceName { get; set; } = null!;
    }
}