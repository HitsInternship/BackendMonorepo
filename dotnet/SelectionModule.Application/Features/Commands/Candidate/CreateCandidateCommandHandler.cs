using MediatR;
using SelectionModule.Contracts.Commands.Candidate;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;

namespace SelectionModule.Application.Features.Commands.Candidate;

public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CandidateEntity>
{
    private readonly ICandidateRepository _candidateRepository;

    public CreateCandidateCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public Task<CandidateEntity> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = new CandidateEntity
        {
            UserId = request.UserId,
            StudentId = request.StudentId,
            Selection = request.SelectionEntity
        };
        
        return Task.FromResult(candidate);
    }
}