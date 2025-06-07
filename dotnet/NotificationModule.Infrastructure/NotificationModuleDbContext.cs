using Microsoft.EntityFrameworkCore;
using NotificationModule.Domain.Entites;

namespace NotificationModule.Infrastructure;

public class NotificationModuleDbContext(DbContextOptions<NotificationModuleDbContext> options) : DbContext(options)
{
    private DbSet<Message> Messages { get; set; }
}