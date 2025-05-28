using MediatR;
using SelectionModule.Domain.Entites;

namespace SelectionModule.Contracts.Commands.Candidate;

public record CreateCandidateCommand(Guid UserId, Guid StudentId, SelectionEntity SelectionEntity)
    : IRequest<CandidateEntity>;