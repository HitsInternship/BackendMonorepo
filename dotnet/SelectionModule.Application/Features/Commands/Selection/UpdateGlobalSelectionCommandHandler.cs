using MediatR;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Selection;

public class UpdateGlobalSelectionCommandHandler : IRequestHandler<UpdateGlobalSelectionCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IGlobalSelectionRepository _globalSelectionRepository;

    public UpdateGlobalSelectionCommandHandler(ISender mediator, IGlobalSelectionRepository globalSelectionRepository)
    {
        _mediator = mediator;
        _globalSelectionRepository = globalSelectionRepository;
    }

    public async Task<Unit> Handle(UpdateGlobalSelectionCommand request, CancellationToken cancellationToken)
    {
        var globalSelections = await _globalSelectionRepository.FindAsync(x => x.IsDeleted == false);

        if (!globalSelections.Any()) throw new BadRequest("There is no active selection.");

        var globalSelection = globalSelections.First();
        
        await _globalSelectionRepository.SoftDeleteAsync(globalSelection.Id);

        foreach (var selection in globalSelection.Selections)
        {
            await _mediator.Send(new ChangeSelectionDateCommand(selection, request.Date), cancellationToken);
        }

        return Unit.Value;
    }
}