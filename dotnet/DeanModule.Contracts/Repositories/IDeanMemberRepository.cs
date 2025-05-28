using DeanModule.Domain.Entities;
using Shared.Contracts.Repositories;

namespace DeanModule.Contracts.Repositories;

public interface IDeanMemberRepository : IBaseEntityRepository<DeanMember>
{
}