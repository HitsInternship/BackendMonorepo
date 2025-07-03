using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Queries;

public record GetSelectionsQuery(
    Guid SelectionId,
    int? GroupNumber,
    SelectionStatus? Status,
    Guid? SemesterId,
    Guid UserId,
    List<string> Roles)
    : IRequest<List<ListedSelectionDto>>;