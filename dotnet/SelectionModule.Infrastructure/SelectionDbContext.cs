using Microsoft.EntityFrameworkCore;
using SelectionModule.Domain.Entites;
using Shared.Domain.Entites;

namespace SelectionModule.Infrastructure;

public class SelectionDbContext(DbContextOptions<SelectionDbContext> options) : DbContext(options)
{
    private DbSet<CandidateEntity> Candidates { get; set; }
    private DbSet<PositionEntity> Positions { get; set; }
    private DbSet<SelectionEntity> Selections { get; set; }
    private DbSet<VacancyEntity> Vacancies { get; set; }
    private DbSet<SelectionCommentEntity> SelectionComments { get; set; }
    private DbSet<VacancyResponseCommentEntity> VacancyResponseComments { get; set; }
    private DbSet<GlobalSelection> GlobalSelections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VacancyEntity>()
            .HasOne(x => x.Position);

        modelBuilder.Entity<CandidateEntity>()
            .HasOne(c => c.Selection)
            .WithOne(s => s.Candidate)
            .HasForeignKey<SelectionEntity>(s => s.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VacancyResponseEntity>()
            .HasOne(vr => vr.Candidate)
            .WithMany(c => c.Responses)
            .HasForeignKey(vr => vr.CandidateId);

        modelBuilder.Entity<VacancyResponseEntity>()
            .HasOne(vr => vr.Vacancy)
            .WithMany(v => v.Responses)
            .HasForeignKey(vr => vr.VacancyId);

        modelBuilder.Entity<SelectionEntity>()
            .HasMany(x => x.Comments)
            .WithOne(s => s.Selection)
            .HasForeignKey(c => c.ParentId);

        modelBuilder.Entity<VacancyResponseEntity>()
            .HasMany(x => x.Comments)
            .WithOne(s => s.VacancyResponse)
            .HasForeignKey(c => c.ParentId);

        modelBuilder.Entity<GlobalSelection>()
            .HasMany(x => x.Selections)
            .WithOne(s => s.GlobalSelection)
            .HasForeignKey(x => x.GlobalSelectionId);
    }
}