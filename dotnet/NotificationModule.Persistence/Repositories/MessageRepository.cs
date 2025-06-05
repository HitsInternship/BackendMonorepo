using Microsoft.EntityFrameworkCore;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Entites;
using NotificationModule.Domain.Enums;
using NotificationModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace NotificationModule.Persistence.Repositories;

public class MessageRepository(NotificationModuleDbContext context)
    : BaseEntityRepository<Message>(context), IMessageRepository
{
    public async Task<IEnumerable<Message>> GetMessagesByStatusAsync(MessageStatus status)
    {
        return await DbSet.Where(x => x.MessageStatus == status).ToListAsync();
    }
}