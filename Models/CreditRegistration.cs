using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mini_Project_Kredit.Models
{
    public class CreditRegistration
    {
        public int idRegistration { get; set; }
        public int idCustomer { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "NIK harus 16 karakter")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "NIK harus berupa 16 digit angka")]
        public string Nik { get; set; } = default!;
        [Required]
        [StringLength(50, ErrorMessage = "Alamat tidak boleh melebihi {1} karakter.")]
        public string FullName { get; set; } = default!;
        [Required]
        [StringLength(160, ErrorMessage = "Nama Desa tidak boleh melebihi {1} karakter.")]
        public string Address { get; set; } = default!;
        [Required]
        [StringLength(13, MinimumLength = 8,
        ErrorMessage = "Nomor telepon minimal 8 dan maksimal 13 digit")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Nomor telepon hanya boleh angka")]
        public string PhoneNumber { get; set; } = default!;
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter")]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
            ErrorMessage = "Password harus mengandung huruf kapital, angka, dan karakter spesial"
        )]
        public string Password { get; set; } = default!;
        public string? VillageId { get; set; }
        public string? VillageName { get; set; }
        [Required]
        public string DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? RegencyName { get; set; }
        public DateTime RegistrationDate { get; set; }


    }
}