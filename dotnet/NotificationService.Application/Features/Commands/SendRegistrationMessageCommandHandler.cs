using MediatR;
using NotificationModule.Contracts.Commands;

namespace NotificationService.Application.Features.Commands;

public class SendRegistrationMessageCommandHandler : IRequestHandler<SendRegistrationMessageCommand, Unit>
{
    public Task<Unit> Handle(SendRegistrationMessageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}