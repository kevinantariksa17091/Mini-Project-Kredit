using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Mini_Project_Kredit.Models
{
    public class DesaApiResponse
    {
        public int total { get; set; }
        public List<DesaApiRow> rows { get; set; } = new();
    }

    public class DesaApiRow
    {
        public string id { get; set; } = string.Empty;
        public string district_id { get; set; } = string.Empty;
        public string desa { get; set; } = string.Empty;
        public string kecamatan { get; set; } = string.Empty;
        public string regency_id { get; set; } = string.Empty;
        public string kabupaten { get; set; } = string.Empty;
        public string id_propinsi { get; set; } = string.Empty;
        public string provinsi { get; set; } = string.Empty;

        // 👉 helper untuk dropdown
        public string DisplayName =>
            $"{desa} - {kecamatan}, {kabupaten}";
    }
}
