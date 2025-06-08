using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using DocumentModule.Contracts.Queries;
using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class DownloadApplicationFileCommandHandler : IRequestHandler<DownloadApplicationFileCommand, FileContentResult>
{
    private readonly ISender _mediator;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public DownloadApplicationFileCommandHandler(ISender mediator, IApplicationRepository applicationRepository,
        IStudentRepository studentRepository)
    {
        _mediator = mediator;
        _applicationRepository = applicationRepository;
        _studentRepository = studentRepository;
    }

    public async Task<FileContentResult> Handle(DownloadApplicationFileCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);
        var student = await _studentRepository.GetByIdAsync(application.StudentId);

        if (!request.Roles.Contains("DeanMember"))
        {
            if (student.UserId != request.UserId)
                throw new Forbidden("You are not allowed to access this page");

            if (application.IsDeleted)
                throw new BadRequest("The application is deleted");
        }

        if (application.DocumentId.HasValue)
        {
            return await _mediator.Send(new GetDocumentQuery(application.DocumentId.Value, DocumentType.Attachement),
                cancellationToken);
        }

        throw new BadRequest("The application file does not exist");
    }
}