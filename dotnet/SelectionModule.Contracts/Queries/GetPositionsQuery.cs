using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetPositionsQuery() : IRequest<List<PositionDto>>;