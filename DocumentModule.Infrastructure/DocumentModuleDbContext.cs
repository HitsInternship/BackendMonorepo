using DocumentModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentModule.Infrastructure
{
    public class DocumentModuleDbContext : DbContext
    {
        public DocumentModuleDbContext(DbContextOptions<DocumentModuleDbContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
