using CompanyModule.Contracts.Repositories;
using DeanModule.Contracts.Commands;
using DeanModule.Domain.Enums;
using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using PracticeModule.Contracts.Repositories;
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
    private readonly IPracticeRepository _practiceRepository;

    public SendChangingPracticeNotificationCommandHandler(ISender mediator, IUserRepository userRepository,
        IStudentRepository studentRepository, IPositionRepository positionRepository,
        ICompanyRepository companyRepository, IPracticeRepository practiceRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _positionRepository = positionRepository;
        _companyRepository = companyRepository;
        _practiceRepository = practiceRepository;
    }

    public async Task<Unit> Handle(SendChangingPracticeNotificationCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.Application.StudentId);
        var user = await _userRepository.GetByIdAsync(student.UserId);

        var oldPractice = await _practiceRepository.GetByIdAsync(request.Application.OldPractice);

        var oldCompany = await _companyRepository.GetByIdAsync(oldPractice.CompanyId);
        var oldPosition = await _positionRepository.GetByIdAsync(oldPractice.PositionId);
        var newCompany = await _companyRepository.GetByIdAsync(request.Application.CompanyId);
        var newPosition = await _positionRepository.GetByIdAsync(request.Application.PositionId);

        await _mediator.Send(
            new SendChangingPracticeCommand(user.Email, oldCompany.Name, oldPosition.Name, newCompany.Name,
                newPosition.Name,
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