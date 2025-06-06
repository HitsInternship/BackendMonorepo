using CompanyModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Infrastructure
{
    public class CompanyModuleDbContext : DbContext
    {
        public DbSet<PartnershipAgreement> Agreements { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Curator> Curators { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public CompanyModuleDbContext(DbContextOptions<CompanyModuleDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().HasOne(appointment => appointment.Timeslot).WithOne(timeslot => timeslot.Appointment).HasForeignKey<Appointment>("timeslotId");
        }
    }
}
