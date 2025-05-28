using MediatR;
using Shared.Contracts.Dtos;

namespace SelectionModule.Contracts.Queries;

public record GetSelectionCommentsQuery(Guid SelectionId, Guid UserId, List<string> Roles) : IRequest<List<CommentDto>>;