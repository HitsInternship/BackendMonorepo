using NotificationModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace NotificationModule.Contracts.Repositories;

public interface IMessageRepository : IBaseEntityRepository<Message>
{
}