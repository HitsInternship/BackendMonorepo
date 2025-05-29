using MediatR;
using Shared.Contracts.Dtos;

namespace DeanModule.Contracts.Queries;

public record GetApplicationCommentsQuery(Guid ApplicationId, Guid UserId) : IRequest<List<CommentDto>>;