using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Enums;
using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.Application;

public class UploadApplicationFileCommandHandler : IRequestHandler<UploadApplicationFileCommand, Guid>
{
    private readonly ISender _mediator;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public UploadApplicationFileCommandHandler(ISender mediator, IApplicationRepository applicationRepository,
        IStudentRepository studentRepository)
    {
        _mediator = mediator;
        _applicationRepository = applicationRepository;
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(UploadApplicationFileCommand request, CancellationToken cancellationToken)
    {
        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        if (application.Status is ApplicationStatus.Rejected or ApplicationStatus.Accepted)
            throw new BadRequest("You cannot update a rejected or accepted application");

        if (application.IsDeleted) throw new BadRequest("You cannot upload a file a deleted application");

        var student = await _studentRepository.GetByIdAsync(application.StudentId);

        if (student.UserId != request.UserId)
            throw new Forbidden("You cannot update this application.");

        var fileId = await _mediator.Send(
            new LoadDocumentCommand(DocumentType.ChangePracticeApplication, request.File.File),
            cancellationToken);

        application.DocumentId = fileId;
        await _applicationRepository.UpdateAsync(application);

        return fileId;
    }
}