using MediatR;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.Repositories;

namespace PracticeModule.Application.Handler.PracticePart;

public class MarkPracticeCommandHandler : IRequestHandler<MarkPracticeCommand, Unit>
{
    private readonly IPracticeRepository _practiceRepository;

    public MarkPracticeCommandHandler(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<Unit> Handle(MarkPracticeCommand command, CancellationToken cancellationToken)
    {
        Domain.Entity.Practice practice = await _practiceRepository.GetByIdAsync(command.practiceId);

        practice.Mark = command.mark;

        await _practiceRepository.UpdateAsync(practice);

        return Unit.Value;
    }
}