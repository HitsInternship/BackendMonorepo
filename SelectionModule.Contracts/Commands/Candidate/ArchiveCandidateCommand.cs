using MediatR;

namespace SelectionModule.Contracts.Commands.Candidate;

public record ArchiveCandidateCommand(Guid CandidateId) : IRequest<Unit>;