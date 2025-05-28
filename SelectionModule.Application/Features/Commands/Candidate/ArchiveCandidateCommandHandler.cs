using MediatR;
using SelectionModule.Contracts.Commands.Candidate;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Candidate;

public class ArchiveCandidateCommandHandler : IRequestHandler<ArchiveCandidateCommand, Unit>
{
    private readonly ICandidateRepository _candidateRepository;

    public ArchiveCandidateCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Unit> Handle(ArchiveCandidateCommand request, CancellationToken cancellationToken)
    {
        if (!await _candidateRepository.CheckIfExistsAsync(request.CandidateId))
            throw new NotFound("Candidate not found");

        var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);

        await _candidateRepository.SoftDeleteAsync(candidate.Id);

        return Unit.Value;
    }
}