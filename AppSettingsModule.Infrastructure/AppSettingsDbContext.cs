using AppSettingsModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppSettingsModule.Infrastructure
{
    public class AppSettingsDbContext : DbContext
    {
        public AppSettingsDbContext(DbContextOptions<AppSettingsDbContext> options) : base(options) { }

        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Settings>()
                .HasIndex(us => us.userId) 
                .IsUnique(); 
        }
    }
}
