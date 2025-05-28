using MediatR;
using SelectionModule.Contracts.Dtos.Requests;

namespace SelectionModule.Contracts.Commands.Vacancy;

public record UpdateVacancyCommand(Guid VacancyId, VacancyRequestDto VacancyRequestDto, Guid? UserId = null) : IRequest<Unit>;