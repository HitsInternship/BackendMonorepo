using MediatR;
using SelectionModule.Contracts.Dtos.Requests;

namespace SelectionModule.Contracts.Commands.Vacancy;

public record CreateVacancyCommand(VacancyRequestDto VacancyRequestDto, Guid? UserId = null) : IRequest<Unit>;