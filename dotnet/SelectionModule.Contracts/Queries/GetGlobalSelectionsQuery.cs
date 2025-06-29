using MediatR;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Queries;

public record GetGlobalSelectionsQuery(bool IsArchived) : IRequest<List<GlobalSelectionDto>>;