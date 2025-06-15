using MediatR;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.Repositories;

namespace PracticeModule.Application.Handler.Practice;

public class MarkPracticesCommandHandler : IRequestHandler<MarkPracticesCommand, Unit>
{
    private readonly IPracticeRepository _practiceRepository;

    public MarkPracticesCommandHandler(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<Unit> Handle(MarkPracticesCommand command, CancellationToken cancellationToken)
    {
        Domain.Entity.Practice practice = await _practiceRepository.GetByIdAsync(command.practiceId);

        practice.Mark = command.mark;

        await _practiceRepository.UpdateAsync(practice);

        return Unit.Value;
    }
}