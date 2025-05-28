using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetSelectionQuery(Guid StudentId) : IRequest<SelectionDto>;