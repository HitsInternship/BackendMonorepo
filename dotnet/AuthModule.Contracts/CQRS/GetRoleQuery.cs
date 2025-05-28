using MediatR;

namespace AuthModule.Contracts.CQRS;

public record GetRoleQuery(Guid UserId) : IRequest<List<string>>;