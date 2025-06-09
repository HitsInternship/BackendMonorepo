using MediatR;
using SelectionModule.Domain.Entites;

namespace SelectionModule.Contracts.Commands.Selection;

public record ChangeSelectionDateCommand(SelectionEntity Selection, DateOnly Date) : IRequest<Unit>;