using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mini_Project_Kredit.Models;
using Mini_Project_Kredit.Services;
using Microsoft.AspNetCore.Components.Forms;


namespace Mini_Project_Kredit.Services
{
    public class CreditRegistrationService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly IEmailSender _emailSender;

        public CreditRegistrationService(IDbContextFactory<AppDbContext> dbFactory, IEmailSender emailSender)
        {
            _dbFactory = dbFactory;
            _emailSender = emailSender;
        }

        public async Task<ServiceResult> RegisterAsync(CreditRegistration model)
        {
            if (string.IsNullOrWhiteSpace(model.username))
                return ServiceResult.Fail("Username wajib diisi.");

            model.username = model.username.Trim();
            model.RegistrationDate = DateTime.Now;

            await using var db = await _dbFactory.CreateDbContextAsync();

            var usernameExists = await db.CreditRegistrations
                .AnyAsync(x => x.username == model.username);

            if (usernameExists)
                return ServiceResult.Fail("Username sudah digunakan.");

            try
            {
                db.CreditRegistrations.Add(model);
                await db.SaveChangesAsync();

                try
                {
                    await _emailSender.SendRegistrationConfirmationAsync(
                        model.Email,
                        model.username
                    );
                }
                catch (Exception emailEx)
                {
                    // Email gagal TIDAK membatalkan registrasi
                    Console.WriteLine($"[Email] Gagal kirim email: {emailEx.Message}");
                }

                return ServiceResult.Ok("Registrasi berhasil.", model.idRegistration);
            }
            catch (DbUpdateException ex)
            {
                var root = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"[Register][DbUpdateException] {root}");

                return ServiceResult.Fail($"Registrasi gagal: {root}");
            }
        }


        // Optional: untuk login sederhana (sementara, karena password idealnya HASH)
        public async Task<CreditRegistration?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            username = username.Trim();

            return await db.CreditRegistrations
                .FirstOrDefaultAsync(x => x.username == username && x.Password == password);
        }

        // Optional: ambil user by id
        public async Task<CreditRegistration?> GetByIdAsync(int idRegistration)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.CreditRegistrations.FirstOrDefaultAsync(x => x.idRegistration == idRegistration);
        }

        // Optional: update profil (contoh)
        public async Task<ServiceResult> UpdateProfileAsync(CreditRegistration updated)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            var existing = await db.CreditRegistrations
                .FirstOrDefaultAsync(x => x.idRegistration == updated.idRegistration);

            if (existing == null)
                return ServiceResult.Fail("Data user tidak ditemukan.");

            // Update field yang boleh diubah
            existing.FullName = updated.FullName;
            existing.Address = updated.Address;
            existing.PhoneNumber = updated.PhoneNumber;

            await db.SaveChangesAsync();
            return ServiceResult.Ok("Profil berhasil diperbarui.", existing.idRegistration);
        }
        public async Task<ServiceResult> UpdateRegistrationAsync(CreditRegistration updated)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            var existing = await db.CreditRegistrations
                .FirstOrDefaultAsync(x => x.idRegistration == updated.idRegistration);

            if (existing == null)
                return ServiceResult.Fail("Data registrasi tidak ditemukan.");

            existing.FullName = updated.FullName;
            existing.Address = updated.Address;
            existing.PhoneNumber = updated.PhoneNumber;

            existing.VillageId = updated.VillageId;
            existing.VillageName = updated.VillageName;
            existing.DistrictName = updated.DistrictName;
            existing.RegencyName = updated.RegencyName;

            existing.ProfileImagePath = updated.ProfileImagePath;

            await db.SaveChangesAsync();
            return ServiceResult.Ok("Data berhasil diperbarui.");
        }
        public async Task<List<CreditRegistration>> GetAllAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.CreditRegistrations
                .AsNoTracking()
                .OrderByDescending(x => x.idRegistration)
                .ToListAsync();
        }
        public async Task<CreditRegistration?> GetByUsernameAsync(string username)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            username = username.Trim();

            return await db.CreditRegistrations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.username == username);
        }
        public async Task<string> SaveProfileImageAsync(IBrowserFile file)
        {
            // 1. Logika di sini adalah:
            //    a. Menentukan path fisik di server (misal: wwwroot/profiles)
            //    b. Menyimpan stream file ke path tersebut.
            //    c. MENGEMBALIKAN PATH RELATIF URL (misal: /profiles/user_123.png)

            // Contoh: return "/uploads/profiles/namafile.jpg";
            throw new NotImplementedException("Implementasi penyimpanan file di server diperlukan.");
        }

    }
}
