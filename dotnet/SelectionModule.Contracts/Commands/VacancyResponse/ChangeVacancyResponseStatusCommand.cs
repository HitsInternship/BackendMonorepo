using MediatR;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Contracts.Commands.VacancyResponse;

public record ChangeVacancyResponseStatusCommand(Guid VacancyResponseId, Guid UserId, VacancyResponseStatus Status)
    : IRequest<Unit>;