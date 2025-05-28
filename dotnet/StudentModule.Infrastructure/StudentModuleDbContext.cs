using Microsoft.EntityFrameworkCore;
using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;

namespace StudentModule.Infrastructure
{
    public class StudentModuleDbContext : DbContext
    {
        public StudentModuleDbContext(DbContextOptions<StudentModuleDbContext> options) : base(options) { }

        public DbSet<StreamEntity> Streams { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<StudentEntity> SStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<StudentEntity>()
                        .HasOne(s => s.Group)
                        .WithMany(g => g.Students)
                        .HasForeignKey(s => s.GroupId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupEntity>()
                       .HasOne(g => g.Stream)
                       .WithMany(s => s.Groups)
                       .HasForeignKey(g => g.StreamId)
                       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
