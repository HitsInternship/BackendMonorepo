using AuthModule.Contracts.Model;
using MediatR;

namespace AuthModule.Contracts.CQRS;

public record CreateAspNetUserQuery : IRequest<CredInfoDTO>
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
}