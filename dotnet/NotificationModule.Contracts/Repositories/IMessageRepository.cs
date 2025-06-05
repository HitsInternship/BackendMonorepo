using NotificationModule.Domain.Entites;
using NotificationModule.Domain.Enums;
using Shared.Contracts.Repositories;

namespace NotificationModule.Contracts.Repositories;

public interface IMessageRepository : IBaseEntityRepository<Message>
{
    Task<IEnumerable<Message>> GetMessagesByStatusAsync(MessageStatus status);
}