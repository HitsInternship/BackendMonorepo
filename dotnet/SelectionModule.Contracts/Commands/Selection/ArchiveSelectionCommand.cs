using MediatR;
using Unit = System.Reactive.Unit;

namespace SelectionModule.Contracts.Commands.Selection;

public record ArchiveSelectionCommand(Guid SelectionId) : IRequest<Unit>, IRequest<MediatR.Unit>;