using MediatR;
using SelectionModule.Contracts.Dtos.Requests;

namespace SelectionModule.Contracts.Commands.Selection;

public record CreateGlobalSelectionCommand(SelectionRequestDto SelectionRequestDto) : IRequest<Unit>;