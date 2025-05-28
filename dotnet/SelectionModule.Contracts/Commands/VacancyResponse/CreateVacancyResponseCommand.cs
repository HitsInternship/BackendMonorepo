using MediatR;

namespace SelectionModule.Contracts.Commands.VacancyResponse;

public record CreateVacancyResponseCommand(Guid UserId, Guid VacancyId) : IRequest<Unit>;