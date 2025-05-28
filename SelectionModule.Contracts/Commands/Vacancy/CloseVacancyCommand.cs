using MediatR;

namespace SelectionModule.Contracts.Commands.Vacancy;

public record CloseVacancyCommand(Guid UserId, Guid VacancyId) : IRequest<Unit>;