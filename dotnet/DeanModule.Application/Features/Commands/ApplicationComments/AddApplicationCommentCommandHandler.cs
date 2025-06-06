using DeanModule.Contracts.Commands.ApplicationComments;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;

namespace DeanModule.Application.Features.Commands.ApplicationComments;

public class AddApplicationCommentCommandHandler : IRequestHandler<AddApplicationCommentCommand, Unit>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationCommentRepository _applicationCommentRepository;

    public AddApplicationCommentCommandHandler(IApplicationRepository applicationRepository,
        IApplicationCommentRepository applicationCommentRepository)
    {
        _applicationRepository = applicationRepository;
        _applicationCommentRepository = applicationCommentRepository;
    }

    public async Task<Unit> Handle(AddApplicationCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new NotFound("Application not found");

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        if (application.StudentId != request.UserId || !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You are not allowed to add application comments");

        await _applicationCommentRepository.AddAsync(new ApplicationComment
        {
            Content = request.Comment,
            UserId = request.UserId,
            ParentId = application.Id,
            Application = application
        });

        return Unit.Value;
    }
}