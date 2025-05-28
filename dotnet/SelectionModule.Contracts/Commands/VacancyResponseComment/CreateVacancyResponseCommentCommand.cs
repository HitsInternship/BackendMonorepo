using MediatR;

namespace SelectionModule.Contracts.Commands.VacancyResponseComment;

public record CreateVacancyResponseCommentCommand(
    Guid VacancyResponseId,
    Guid UserId,
    string Comment,
    List<string> Roles) : IRequest<Unit>;