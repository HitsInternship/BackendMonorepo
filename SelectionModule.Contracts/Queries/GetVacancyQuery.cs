using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetVacancyQuery(Guid VacancyId) : IRequest<VacancyDto>;