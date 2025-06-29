using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Queries;

public record GetGlobalSelectionQuery(
    Guid Id,
    int? GroupNumber,
    SelectionStatus? Status,
    Guid? SemesterId,
    Guid UserId,
    List<string> Roles) : IRequest<GlobalSelectionResponseDto>;