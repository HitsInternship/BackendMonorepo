using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Queries;

public record GetSelectionsQuery(int? GroupNumber, SelectionStatus? Status, Guid? SemesterId)
    : IRequest<List<ListedSelectionDto>>;