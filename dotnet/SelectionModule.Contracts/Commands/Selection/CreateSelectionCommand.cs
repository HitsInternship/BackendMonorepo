using MediatR;

namespace SelectionModule.Contracts.Commands.Selection;

public record CreateSelectionCommand(Guid StudentId, DateOnly Deadline) : IRequest<Unit>;