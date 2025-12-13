using Microsoft.EntityFrameworkCore;
using Mini_Project_Kredit.Models;

namespace Mini_Project_Kredit.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CreditRegistration> CreditRegistrations => Set<CreditRegistration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique username (wajib untuk anti-race condition)
            modelBuilder.Entity<CreditRegistration>()
                .HasIndex(x => x.username)
                .IsUnique();

            // Optional: kalau NIK juga harus unik, aktifkan ini
            // modelBuilder.Entity<CreditRegistration>()
            //     .HasIndex(x => x.Nik)
            //     .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}