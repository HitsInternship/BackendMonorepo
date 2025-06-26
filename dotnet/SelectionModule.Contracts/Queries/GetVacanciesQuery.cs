using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetVacanciesQuery(
    Guid UserId,
    List<string> Roles,
    bool IsClosed,
    bool IsArchived,
    int Page,
    Guid? CompanyId = null,
    Guid? PositionId = null)
    : IRequest<VacanciesDto>;