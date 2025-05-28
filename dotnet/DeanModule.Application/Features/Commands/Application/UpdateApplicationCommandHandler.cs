using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;

namespace DeanModule.Application.Features.Commands.Application;

public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, Unit>
{
    private readonly IApplicationRepository _applicationRepository;

    public UpdateApplicationCommandHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        //todo: add role check
        if (application.StudentId != request.UserId)
            throw new Forbidden("You cannot update this application.");

        UpdateApplication(application, request.ApplicationRequestDto);

        await _applicationRepository.UpdateAsync(application);

        return Unit.Value;
    }

    private static void UpdateApplication(ApplicationEntity application, ApplicationRequestDto dto)
    {
        application.Date = dto.Date;
        application.Description = dto.Description;
        application.PositionId = dto.PositionId;
        application.CompanyId = dto.CompanyId;
        //todo: add document
    }
}