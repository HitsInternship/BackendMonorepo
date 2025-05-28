using AuthModule.Contracts.Model;
using MediatR;

namespace AuthModule.Contracts.CQRS;

public record LoginDTO : IRequest<LoginResponseDTO>
{
  public string Login { get; init; }
  public string Password { get; init; }
}