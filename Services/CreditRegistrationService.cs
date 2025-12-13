using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mini_Project_Kredit.Models;

namespace Mini_Project_Kredit.Services
{
    public class CreditRegistrationService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public CreditRegistrationService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ServiceResult> RegisterAsync(CreditRegistration model)
        {
            // Guard basic
            if (string.IsNullOrWhiteSpace(model.username))
                return ServiceResult.Fail("Username wajib diisi.");

            // Normalisasi username (biar konsisten, optional tapi bagus)
            model.username = model.username.Trim();

            // ✅ Isi otomatis di backend (WAJIB)
            model.RegistrationDate = DateTime.Now;

            await using var db = await _dbFactory.CreateDbContextAsync();

            // ✅ Cek username sudah ada (database check)
            var usernameExists = await db.CreditRegistrations
                .AnyAsync(x => x.username == model.username);

            if (usernameExists)
                return ServiceResult.Fail("Username sudah digunakan. Silakan pilih username lain.");

            // Optional: cek NIK unik (kalau memang requirement)
            // var nikExists = await db.CreditRegistrations.AnyAsync(x => x.Nik == model.Nik);
            // if (nikExists) return ServiceResult.Fail("NIK sudah terdaftar.");

            try
            {
                db.CreditRegistrations.Add(model);
                await db.SaveChangesAsync();

                return ServiceResult.Ok("Registrasi berhasil.", model.idRegistration);
            }
            catch (DbUpdateException)
            {
                // Anti race condition: kalau 2 request barengan, unique index bisa melempar error
                return ServiceResult.Fail("Registrasi gagal. Username sudah digunakan.");
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
    }
}
