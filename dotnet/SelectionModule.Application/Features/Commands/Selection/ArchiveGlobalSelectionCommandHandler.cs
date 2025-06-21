using MediatR;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Dtos.Requests;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Selection;

public class ArchiveGlobalSelectionCommandHandler : IRequestHandler<ArchiveGlobalSelectionCommand, Unit>
{
    private readonly ISender _sender;
    private readonly IGlobalSelectionRepository _globalSelectionRepository;

    public ArchiveGlobalSelectionCommandHandler(ISender sender, IGlobalSelectionRepository globalSelectionRepository)
    {
        _sender = sender;
        _globalSelectionRepository = globalSelectionRepository;
    }

    public async Task<Unit> Handle(ArchiveGlobalSelectionCommand request, CancellationToken cancellationToken)
    {
        var globalSelection = await _globalSelectionRepository.GetActiveSelectionAsync();

        if (globalSelection == null) throw new BadRequest("There is no active selection.");

        await _globalSelectionRepository.SoftDeleteAsync(globalSelection.Id);

        foreach (var selection in globalSelection.Selections)
        {
            await _sender.Send(new ArchiveSelectionCommand(selection.Id), cancellationToken);
        }

        return Unit.Value;
    }
}