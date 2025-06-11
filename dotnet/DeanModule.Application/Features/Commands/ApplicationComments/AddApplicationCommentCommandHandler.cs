using DeanModule.Contracts.Commands.ApplicationComments;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.ApplicationComments;

public class AddApplicationCommentCommandHandler : IRequestHandler<AddApplicationCommentCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationCommentRepository _applicationCommentRepository;

    public AddApplicationCommentCommandHandler(IApplicationRepository applicationRepository,
        IApplicationCommentRepository applicationCommentRepository, IUserRepository userRepository, ISender mediator,
        IStudentRepository studentRepository)
    {
        _applicationRepository = applicationRepository;
        _applicationCommentRepository = applicationCommentRepository;
        _userRepository = userRepository;
        _mediator = mediator;
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(AddApplicationCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new NotFound("Application not found");

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        var student = await _studentRepository.GetByIdAsync(application.StudentId);

        if (student.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You are not allowed to add application comments");

        await _applicationCommentRepository.AddAsync(new ApplicationComment
        {
            Content = request.Comment,
            UserId = request.UserId,
            ParentId = application.Id,
            Application = application
        });


        if (student.UserId != request.UserId)
        {
            var userToSend = await _userRepository.GetByIdAsync(student.UserId);
            var userFromSend = await _userRepository.GetByIdAsync(request.UserId);

            await _mediator.Send(
                new SendNewCommentMessageCommand(userToSend.Email, CommentType.application, request.Comment,
                    userFromSend.Surname + " " + userFromSend.Name, application.Id), cancellationToken);
        }

        return Unit.Value;
    }
}