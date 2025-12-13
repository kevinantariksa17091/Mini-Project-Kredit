using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Project_Kredit.Models
{
    public class CreditRegistrationDTO
    {
        public int idCustomer { get; set; }
        public string Nik { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}