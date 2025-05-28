using MediatR;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Selection;

public class ChangeSelectionCommandHandler : IRequestHandler<ChangeSelectionCommand, Unit>
{
    private readonly ISelectionRepository _selectionRepository;

    public ChangeSelectionCommandHandler(ISelectionRepository selectionRepository)
    {
        _selectionRepository = selectionRepository;
    }

    public async Task<Unit> Handle(ChangeSelectionCommand request, CancellationToken cancellationToken)
    {
        if (!await _selectionRepository.CheckIfExistsAsync(request.SelectionId))
            throw new NotFound("Selection not found");

        var selection = await _selectionRepository.GetByIdAsync(request.SelectionId);

        if (selection.Candidate.UserId != request.UserId || !request.Roles.Contains("DeanMember"))
            throw new Forbidden("This user is not allowed to change the selection");

        selection.SelectionStatus = request.Status;

        await _selectionRepository.UpdateAsync(selection);

        return Unit.Value;
    }
}