using MediatR;

namespace SelectionModule.Contracts.Dtos.Requests;

public record ArchiveGlobalSelectionCommand(Guid Id) : IRequest<Unit>;