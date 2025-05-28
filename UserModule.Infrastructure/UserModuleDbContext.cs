using Microsoft.EntityFrameworkCore;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Infrastructure
{
    public class UserModuleDbContext : DbContext
    {
        public UserModuleDbContext(DbContextOptions<UserModuleDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(User => User.Roles).WithMany(Role => Role.Users).UsingEntity("UserRole");

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
                    RoleName = RoleName.Student
                },
                new Role
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000000"),
                    RoleName = RoleName.Intern
                },
                new Role
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000000"),
                    RoleName = RoleName.Curator
                },
                new Role
                {
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000000"),
                    RoleName = RoleName.DeanMember
                },
                new Role
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000000"),
                    RoleName = RoleName.CompanyRepresenter
                },
                new Role
                {
                    Id = Guid.Parse("60000000-0000-0000-0000-000000000000"),
                    RoleName = RoleName.Candidate
                }
            );
        }
    }
}
