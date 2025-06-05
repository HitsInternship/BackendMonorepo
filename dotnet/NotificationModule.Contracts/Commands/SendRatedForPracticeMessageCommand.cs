using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendRatedForPracticeMessageCommand(string Email, string Rate, Guid PractiseId) : IRequest<Unit>;