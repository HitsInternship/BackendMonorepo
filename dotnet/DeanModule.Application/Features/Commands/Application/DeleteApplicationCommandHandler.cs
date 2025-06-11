using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.Application;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public DeleteApplicationCommandHandler(IApplicationRepository applicationRepository, ISender mediator,
        IStudentRepository studentRepository)
    {
        _applicationRepository = applicationRepository;
        _mediator = mediator;
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        if (!request.roles.Contains("DeanMember"))
        {
            var student = await _studentRepository.GetByIdAsync(application.StudentId);

            if (student.UserId != request.UserId)
                throw new Forbidden("You cannot delete this application.");

            if (student.UserId == request.UserId && request.IsArchive)
                throw new BadRequest("You cannot archive application.");
        }

        if (request.IsArchive)
        {
            if (!request.roles.Contains("DeanMember"))
                throw new Forbidden("You cannot archivate this application.");

            await _applicationRepository.SoftDeleteAsync(application.Id);
        }
        else
        {
            await _applicationRepository.DeleteAsync(application);
            if (application.DocumentId.HasValue)
            {
                await _mediator.Send(new RemoveDocumentCommand(application.DocumentId.Value, DocumentType.Attachement),
                    cancellationToken);
            }
        }

        return Unit.Value;
    }
}