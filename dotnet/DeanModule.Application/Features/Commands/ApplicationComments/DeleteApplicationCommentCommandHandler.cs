using DeanModule.Contracts.Commands.ApplicationComments;
using DeanModule.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;

namespace DeanModule.Application.Features.Commands.ApplicationComments;

public class DeleteApplicationCommentCommandHandler : IRequestHandler<DeleteApplicationCommentCommand, Unit>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationCommentRepository _applicationCommentRepository;

    public DeleteApplicationCommentCommandHandler(IApplicationCommentRepository applicationCommentRepository, IApplicationRepository applicationRepository)
    {
        _applicationCommentRepository = applicationCommentRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(DeleteApplicationCommentCommand request, CancellationToken cancellationToken)
    {
        if(!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new NotFound("Application not found");
        
        if (!await _applicationCommentRepository.CheckIfExistsAsync(request.ApplicationCommentId))
            throw new NotFound("Comment  not  found");

        var applicationComment = await _applicationCommentRepository.GetByIdAsync(request.ApplicationCommentId);

        if (applicationComment.UserId != request.UserId) throw new Forbidden("You cannot delete this comment");

        await _applicationCommentRepository.DeleteAsync(applicationComment);

        return Unit.Value;
    }
}