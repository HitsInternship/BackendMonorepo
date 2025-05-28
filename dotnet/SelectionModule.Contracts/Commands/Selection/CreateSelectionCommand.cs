using MediatR;

namespace SelectionModule.Contracts.Commands.Selection;

public record CreateSelectionCommand(Guid StudentId, DateTime Deadline) : IRequest<Unit>;