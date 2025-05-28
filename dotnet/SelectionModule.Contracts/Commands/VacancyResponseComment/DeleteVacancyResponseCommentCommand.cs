using MediatR;

namespace SelectionModule.Contracts.Commands.VacancyResponseComment;

public record DeleteVacancyResponseCommentCommand(Guid VacancyResponseId, Guid CommentId, Guid UserId) : IRequest<Unit>;