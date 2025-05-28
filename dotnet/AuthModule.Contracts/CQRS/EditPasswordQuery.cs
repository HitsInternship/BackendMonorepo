using MediatR;

namespace AuthModule.Contracts.CQRS;

public class EditPasswordQuery : IRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string Login { get; set; }
    public Guid UserId { get; set; }
}