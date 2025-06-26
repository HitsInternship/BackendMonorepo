using MediatR;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace PracticeModule.Application.Handler.Practice;

public class MarkPracticeCommandHandler : IRequestHandler<MarkPracticeCommand, Unit>
{
    private readonly IPracticeRepository _practiceRepository;

    public MarkPracticeCommandHandler(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<Unit> Handle(MarkPracticeCommand command, CancellationToken cancellationToken)
    {
        Domain.Entity.Practice? practice = (await _practiceRepository.ListAllAsync()).Where(practice => practice.Id == command.practiceId).FirstOrDefault();
        if (practice == null) { throw new NotFound("No practice with such id"); }

        practice.Mark = command.mark;

        await _practiceRepository.UpdateAsync(practice);

        return Unit.Value;
    }
}