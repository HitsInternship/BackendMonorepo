using CompanyModule.Contracts.Repositories;
using DeanModule.Contracts.Commands;
using DeanModule.Domain.Enums;
using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using SelectionModule.Contracts.Repositories;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Commands;

public class
    SendChangingPracticeNotificationCommandHandler : IRequestHandler<SendChangingPracticeNotificationCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;

    public SendChangingPracticeNotificationCommandHandler(ISender mediator, IUserRepository userRepository,
        IStudentRepository studentRepository, IPositionRepository positionRepository,
        ICompanyRepository companyRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Unit> Handle(SendChangingPracticeNotificationCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.Application.StudentId);
        var user = await _userRepository.GetByIdAsync(student.UserId);
        var oldCompany = "Old Company"; //todo: get from current practice
        var oldPosition = "Old Position";
        var newCompany = await _companyRepository.GetByIdAsync(request.Application.CompanyId);
        var newPosition = await _positionRepository.GetByIdAsync(request.Application.PositionId);

        await _mediator.Send(
            new SendChangingPracticeCommand(user.Email, oldCompany, oldPosition, newCompany.Name, newPosition.Name,
                GetStatus(request.Application.Status)), cancellationToken);

        return Unit.Value;
    }

    private static ChangingPracticeStatusType GetStatus(ApplicationStatus status)
    {
        switch (status)
        {
            case ApplicationStatus.UnderConsideration:
                return ChangingPracticeStatusType.in_progress;
            case ApplicationStatus.Rejected:
                return ChangingPracticeStatusType.denied;
            case ApplicationStatus.Accepted:
                return ChangingPracticeStatusType.completed;
        }

        return ChangingPracticeStatusType.in_progress;
    }
}