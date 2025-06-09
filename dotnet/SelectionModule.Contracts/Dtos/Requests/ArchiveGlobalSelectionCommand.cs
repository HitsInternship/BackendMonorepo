using MediatR;

namespace SelectionModule.Contracts.Dtos.Requests;

public record ArchiveGlobalSelectionCommand() : IRequest<Unit>;