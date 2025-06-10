using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetMySelectionQuery(Guid UserId) : IRequest<SelectionDto>;