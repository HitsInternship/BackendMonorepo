using DeanModule.Contracts.Commands.ApplicationComments;
using DeanModule.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;

namespace DeanModule.Application.Features.Commands.ApplicationComments;

public class UpdateApplicationCommentCommandHandler : IRequestHandler<UpdateApplicationCommentCommand, Unit>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationCommentRepository _applicationCommentRepository;

    public UpdateApplicationCommentCommandHandler(IApplicationCommentRepository applicationCommentRepository,
        IApplicationRepository applicationRepository)
    {
        _applicationCommentRepository = applicationCommentRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(UpdateApplicationCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new NotFound("Application not found");


        if (!await _applicationCommentRepository.CheckIfExistsAsync(request.ApplicationCommentId))
            throw new NotFound("Comment not found");

        var comment = await _applicationCommentRepository.GetByIdAsync(request.ApplicationCommentId);

        if (comment.UserId != request.UserId) throw new Forbidden("You cannot edit this comment");

        comment.Content = request.Content;

        await _applicationCommentRepository.UpdateAsync(comment);

        return Unit.Value;
    }
}