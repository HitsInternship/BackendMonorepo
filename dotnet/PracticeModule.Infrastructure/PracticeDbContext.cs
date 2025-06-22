using Microsoft.EntityFrameworkCore;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Infrastructure;

public class PracticeDbContext : DbContext
{
    public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options) { }
    
    public DbSet<Practice> Practice { get; set; }
    public DbSet<PracticeDiary> PracticeDiary { get; set; }
    public DbSet<StudentPracticeCharacteristic> StudentPracticeCharacteristic { get; set; }
    public DbSet<PracticeDiaryComment> PracticeDiaryComment { get; set; }
    public DbSet<StudentPracticeCharacteristicComment> StudentPracticeCharacteristicComment { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PracticeDiaryComment>().HasOne<PracticeDiary>().WithMany(x => x.Comment).HasForeignKey(comment => comment.DiaryId);
        modelBuilder.Entity<StudentPracticeCharacteristicComment>().HasOne<StudentPracticeCharacteristic>().WithMany(x => x.PracticeComment).HasForeignKey(comment => comment.PracticeCharacteristicId);
    }
}