using MediatR;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.Selection;

public class ChangeSelectionDateCommandHandler : IRequestHandler<ChangeSelectionDateCommand, Unit>
{
    private readonly ISelectionRepository _selectionRepository;

    public ChangeSelectionDateCommandHandler(ISelectionRepository selectionRepository)
    {
        _selectionRepository = selectionRepository;
    }

    public async Task<Unit> Handle(ChangeSelectionDateCommand request, CancellationToken cancellationToken)
    {
        request.Selection.DeadLine = request.Date;

        await _selectionRepository.UpdateAsync(request.Selection);

        return Unit.Value;
    }
}