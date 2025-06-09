using MediatR;
using SelectionModule.Domain.Entites;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Commands.Selection;

public record ChangeSelectionCommand(Guid UserId, Guid SelectionId, SelectionStatus Status, List<string> Roles)
    : IRequest<Unit>
{
}