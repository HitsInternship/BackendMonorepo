using MediatR;
using SelectionModule.Contracts.Commands.VacancyResponseComment;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.VacancyResponseComment;

public class DeleteVacancyResponseCommentCommandHandler : IRequestHandler<DeleteVacancyResponseCommentCommand, Unit>
{
    private readonly IVacancyResponseRepository _vacancyResponseRepository;
    private readonly IVacancyResponseCommentRepository _vacancyResponseCommentRepository;


    public DeleteVacancyResponseCommentCommandHandler(IVacancyResponseRepository vacancyResponseRepository,
        IVacancyResponseCommentRepository vacancyResponseCommentRepository)
    {
        _vacancyResponseRepository = vacancyResponseRepository;
        _vacancyResponseCommentRepository = vacancyResponseCommentRepository;
    }

    public async Task<Unit> Handle(DeleteVacancyResponseCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _vacancyResponseRepository.CheckIfExistsAsync(request.VacancyResponseId))
            throw new NotFound("Selection not found");

        if (!await _vacancyResponseCommentRepository.CheckIfExistsAsync(request.CommentId))
            throw new NotFound("Selection not found");

        var comment = await _vacancyResponseCommentRepository.GetByIdAsync(request.CommentId);

        if (comment.UserId != request.UserId)
            throw new Forbidden("You do not have access to leave comment");

        await _vacancyResponseCommentRepository.DeleteAsync(comment);

        return Unit.Value;
    }
}