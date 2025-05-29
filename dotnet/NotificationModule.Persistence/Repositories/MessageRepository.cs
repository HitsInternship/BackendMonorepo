using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Entites;
using NotificationModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace NotificationModule.Persistence.Repositories;

public class MessageRepository(NotificationModuleDbContext context)
    : BaseEntityRepository<Message>(context), IMessageRepository
{
}