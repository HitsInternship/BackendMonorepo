using AuthModule.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace UserInfrastructure

{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        
    }
}