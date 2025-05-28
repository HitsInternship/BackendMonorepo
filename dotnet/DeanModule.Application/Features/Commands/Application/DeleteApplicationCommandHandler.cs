using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;

namespace DeanModule.Application.Features.Commands.Application;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, Unit>
{
    private readonly IApplicationRepository _applicationRepository;

    public DeleteApplicationCommandHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);
        
        if (!request.roles.Contains("DeanMember"))
        {
            if (application.StudentId != request.UserId)
                throw new Forbidden("You cannot delete this application.");
            
            if (application.StudentId == request.UserId && request.IsArchive)
                throw new BadRequest("You cannot archive application.");
        }

        if (request.IsArchive)
            await _applicationRepository.SoftDeleteAsync(application.Id);
        else
            await _applicationRepository.DeleteAsync(application);

        return Unit.Value;
    }
}