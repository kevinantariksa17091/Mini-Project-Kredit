using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Project_Kredit.Models
{
    public class InixTokenResponse
    {
        public int status { get; set; }
        public string pesan { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
    }
}