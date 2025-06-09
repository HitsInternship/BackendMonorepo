using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetSelectionQuery(Guid StudentId, Guid UserId, List<string> Roles) : IRequest<SelectionDto>;