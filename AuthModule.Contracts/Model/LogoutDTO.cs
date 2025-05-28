using MediatR;

namespace AuthModule.Contracts.Model;

public class LogoutDTO : IRequest<Unit>
{
    public Guid UserId { get; set; }
}