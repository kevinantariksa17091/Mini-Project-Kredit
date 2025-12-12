using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mini_Project_Kredit.Models // <-- Tambahkan { di sini
{ 
    public class AuthResponse
    {
        public int status { get; set; }
        public string pesan { get; set; }
        public string token { get; set; }
    }
} 