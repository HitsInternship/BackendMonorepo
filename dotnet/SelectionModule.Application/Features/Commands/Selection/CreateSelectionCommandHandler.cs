using MediatR;
using SelectionModule.Contracts.Commands.Candidate;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Domain.Enums;

namespace SelectionModule.Application.Features.Commands.Selection;

public class CreateSelectionCommandHandler : IRequestHandler<CreateSelectionCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly ISelectionRepository _selectionRepository;

    public CreateSelectionCommandHandler(IMediator mediator, ISelectionRepository selectionRepository)
    {
        _mediator = mediator;
        _selectionRepository = selectionRepository;
    }

    public async Task<Unit> Handle(CreateSelectionCommand request, CancellationToken cancellationToken)
    {
        var selections = new List<SelectionEntity>();

        foreach (var student in request.Students)
        {
            var selection = new SelectionEntity
            {
                DeadLine = request.GlobalSelection.EndDate,
                SelectionStatus = SelectionStatus.Inactive,
                CandidateId = default,
                Candidate = null,
                GlobalSelectionId = request.GlobalSelection.Id,
                GlobalSelection = request.GlobalSelection,
            };

            var candidate = await _mediator.Send(new CreateCandidateCommand(student.UserId, student.Id, selection),
                cancellationToken);

            selection.CandidateId = candidate.Id;
            selection.Candidate = candidate;

            selections.Add(selection);
        }

        await _selectionRepository.AddRangeAsync(selections);

        return Unit.Value;
    }
}