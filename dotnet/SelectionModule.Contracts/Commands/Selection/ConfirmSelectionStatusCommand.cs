using MediatR;

namespace SelectionModule.Contracts.Commands.Selection;

public record ConfirmSelectionStatusCommand(Guid SelectionId) : IRequest<Unit>;