using MediatR;

namespace SelectionModule.Contracts.Commands.Vacancy;

public record DeleteVacancyCommand(Guid VacancyId, bool ToArchive, Guid? UserId = null) : IRequest<Unit>;