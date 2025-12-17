using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mini_Project_Kredit.Models;
using Microsoft.EntityFrameworkCore;

namespace Mini_Project_Kredit.Services
{
    public class AuthResult
    {
        public int status { get; set; }
        public string token { get; set; } = "";
        public string pesan { get; set; } = "";
    }

    public class AuthService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public AuthService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<AuthResult?> Login(string username, string password)
        {
            try
            {
                username = (username ?? "").Trim();
                password = password ?? "";

                await using var db = await _dbFactory.CreateDbContextAsync();

                var user = await db.CreditRegistrations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.username == username);

                if (user == null)
                    return new AuthResult { status = 0, pesan = "Username tidak ditemukan." };

                // PERHATIAN: ini plaintext. Untuk production harus hash.
                if (user.Password != password)
                    return new AuthResult { status = 0, pesan = "Password salah." };

                // token sederhana untuk demo (lebih baik pakai JWT)
                var token = Guid.NewGuid().ToString("N");

                return new AuthResult
                {
                    status = 1,
                    token = token,
                    pesan = "Login berhasil."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService.Login] {ex}");
                return null;
            }
        }
    }
}