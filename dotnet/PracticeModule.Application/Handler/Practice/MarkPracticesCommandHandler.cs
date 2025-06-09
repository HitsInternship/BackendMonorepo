using AutoMapper;
using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using Shared.Domain.Exceptions;

namespace PracticeModule.Application.Handler.PracticePart;

public class MarkPracticesCommandHandler : IRequestHandler<MarkPracticesCommand, Unit>
{
    private readonly IPracticeRepository _practiceRepository;

    public MarkPracticesCommandHandler(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<Unit> Handle(MarkPracticesCommand command, CancellationToken cancellationToken)
    {
        Practice practice = await _practiceRepository.GetByIdAsync(command.practiceId);

        practice.Mark = command.mark;

        await _practiceRepository.UpdateAsync(practice);

        return Unit.Value;
    }
}