using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetVacancyResponsesQuery(Guid VacancyId) : IRequest<List<VacancyResponseDto>>;