using DeanModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeanModule.Infrastructure;

public class DeanModuleDbContext(DbContextOptions<DeanModuleDbContext> options) : DbContext(options)
{
    public DbSet<DeanMember> DeanMembers { get; set; }
    public DbSet<SemesterEntity> Semesters { get; set; }
    public DbSet<ApplicationEntity> Applications { get; set; }
    public DbSet<StreamSemesterEntity> StreamSemesters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StreamSemesterEntity>()
            .HasOne(s => s.SemesterEntity)
            .WithMany() 
            .HasForeignKey(s => s.SemesterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}