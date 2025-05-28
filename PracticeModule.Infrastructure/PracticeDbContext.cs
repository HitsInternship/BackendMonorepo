using Microsoft.EntityFrameworkCore;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Infrastructure;

public class PracticeDbContext : DbContext
{
    public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options) { }
    
    public DbSet<Practice> Practice { get; set; }
    public DbSet<PracticeDiary> PracticeDiary { get; set; }
    public DbSet<StudentPracticeCharacteristic> StudentPracticeCharacteristic { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentPracticeCharacteristic>().HasOne(x => x.Practice)
            .WithMany(x => x.StudentPracticeCharacteristics);
        
        modelBuilder.Entity<PracticeDiary>().HasOne(x => x.Practice).WithMany(x => x.PracticeDiaries);
    }
}