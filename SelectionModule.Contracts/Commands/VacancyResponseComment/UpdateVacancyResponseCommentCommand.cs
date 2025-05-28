using MediatR;

namespace SelectionModule.Contracts.Commands.VacancyResponseComment;

public record UpdateVacancyResponseCommentCommand(Guid VacancyResponseId, Guid CommentId, Guid UserId, string Comment)
    : IRequest<Unit>;