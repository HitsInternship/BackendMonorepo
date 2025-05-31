using AuthModule.Contracts.Model;
using MediatR;

namespace AuthModule.Contracts.CQRS;

public record CreateAspNetUserQuery(Guid UserId, string Email, string? Password = null) : IRequest<CredInfoDTO>;