using MediatR;

namespace SelectionModule.Contracts.Commands.Selection;

public record ArchiveSelectionCommand(Guid SelectionId) : IRequest<Unit>;