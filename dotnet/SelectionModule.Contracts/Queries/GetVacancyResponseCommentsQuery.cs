using MediatR;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Queries;

public record GetVacancyResponseCommentsQuery(Guid VacancyResponseId, Guid UserId, List<string> Roles)
    : IRequest<List<CommentDto>>;