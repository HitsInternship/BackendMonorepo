using System.Runtime.Intrinsics.X86;
using DeanModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeanModule.Infrastructure;

public class DeanModuleDbContext(DbContextOptions<DeanModuleDbContext> options) : DbContext(options)
{
    private DbSet<DeanMember> DeanMembers { get; set; }
    private DbSet<SemesterEntity> Semesters { get; set; }
    private DbSet<ApplicationEntity> Applications { get; set; }
    private DbSet<StreamSemesterEntity> StreamSemesters { get; set; }
    private DbSet<ApplicationComment> ApplicationComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StreamSemesterEntity>()
            .HasOne(s => s.SemesterEntity)
            .WithMany()
            .HasForeignKey(s => s.SemesterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApplicationEntity>()
            .HasMany(x => x.Comments)
            .WithOne(x => x.Application)
            .HasForeignKey(x => x.ParentId);
    }
}