using CompanyModule.Contracts.Repositories;
using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DeanModule.Domain.Enums;
using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using SelectionModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands.Application;

public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;

    public UpdateApplicationStatusCommandHandler(IApplicationRepository applicationRepository, ISender mediator,
        IUserRepository userRepository, IPositionRepository positionRepository, ICompanyRepository companyRepository)
    {
        _applicationRepository = applicationRepository;
        _mediator = mediator;
        _userRepository = userRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Unit> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        application.Status = request.Status;

        await _applicationRepository.UpdateAsync(application);

        await SendChangingPracticeCommand(application, cancellationToken);

        return Unit.Value;
    }

    private async Task SendChangingPracticeCommand(ApplicationEntity application, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(application.StudentId);

        //todo : get practice

        var newCompany = await _companyRepository.GetByIdAsync(application.CompanyId);
        var newPosition = await _positionRepository.GetByIdAsync(application.PositionId);

        await _mediator.Send(
            new SendChangingPracticeCommand(user.Email, "Old company", "OldPosition", newCompany.Name, newPosition.Name,
                GetChangingPracticeStatusType(application.Status)),
            cancellationToken);
    }

    private ChangingPracticeStatusType GetChangingPracticeStatusType(ApplicationStatus status)
    {
        return status switch
        {
            ApplicationStatus.Accepted => ChangingPracticeStatusType.completed,
            ApplicationStatus.Rejected => ChangingPracticeStatusType.denied,
            ApplicationStatus.UnderConsideration => ChangingPracticeStatusType.in_progress,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}