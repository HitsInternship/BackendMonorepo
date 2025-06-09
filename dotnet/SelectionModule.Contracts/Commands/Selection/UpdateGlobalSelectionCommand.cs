using MediatR;

namespace SelectionModule.Contracts.Commands.Selection;

public record UpdateGlobalSelectionCommand(DateOnly Date) : IRequest<Unit>;