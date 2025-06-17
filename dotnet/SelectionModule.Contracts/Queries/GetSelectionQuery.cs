using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetSelectionQuery(Guid SelectionId, Guid UserId, List<string> Roles, Guid? StudentId) : IRequest<SelectionDto>;