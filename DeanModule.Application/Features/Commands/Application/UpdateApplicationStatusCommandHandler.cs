using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using MediatR;

namespace DeanModule.Application.Features.Commands.Application;

public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, Unit>
{
    private readonly IApplicationRepository _applicationRepository;

    public UpdateApplicationStatusCommandHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        application.Status = request.Status;

        await _applicationRepository.UpdateAsync(application);

        return Unit.Value;
    }
}