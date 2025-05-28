using MediatR;
using SelectionModule.Contracts.Commands.Candidate;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Selection;

public class ArchiveSelectionCommandHandler : IRequestHandler<ArchiveSelectionCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly ISelectionRepository _selectionRepository;


    public ArchiveSelectionCommandHandler(IMediator mediator, ISelectionRepository selectionRepository)
    {
        _mediator = mediator;
        _selectionRepository = selectionRepository;
    }

    public async Task<Unit> Handle(ArchiveSelectionCommand request, CancellationToken cancellationToken)
    {
        if (!await _selectionRepository.CheckIfExistsAsync(request.SelectionId))
            throw new NotFound("Selection not found");

        var selection = await _selectionRepository.GetByIdAsync(request.SelectionId);

        await _mediator.Send(new ArchiveCandidateCommand(selection.CandidateId), cancellationToken);

        await _selectionRepository.SoftDeleteAsync(selection.Id);

        return Unit.Value;
    }
}