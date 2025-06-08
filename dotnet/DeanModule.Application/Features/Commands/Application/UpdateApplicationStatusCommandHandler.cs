using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using MediatR;

namespace DeanModule.Application.Features.Commands.Application;

public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, Unit>
{
    private readonly ISender _sender;
    private readonly IApplicationRepository _applicationRepository;

    public UpdateApplicationStatusCommandHandler(IApplicationRepository applicationRepository, ISender sender)
    {
        _applicationRepository = applicationRepository;
        _sender = sender;
    }

    public async Task<Unit> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        application.Status = request.Status;

        await _applicationRepository.UpdateAsync(application);

        await _sender.Send(new SendChangingPracticeNotificationCommand(application), cancellationToken);

        return Unit.Value;
    }
}