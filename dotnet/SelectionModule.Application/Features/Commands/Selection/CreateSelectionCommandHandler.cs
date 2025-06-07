using MediatR;
using SelectionModule.Contracts.Commands.Candidate;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Domain.Enums;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.Selection;

public class CreateSelectionCommandHandler : IRequestHandler<CreateSelectionCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly ISelectionRepository _selectionRepository;
    private readonly IStudentRepository _studentRepository;

    public CreateSelectionCommandHandler(IMediator mediator, ISelectionRepository selectionRepository,
        IStudentRepository studentRepository)
    {
        _mediator = mediator;
        _selectionRepository = selectionRepository;
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(CreateSelectionCommand request, CancellationToken cancellationToken)
    {
        if (!await _studentRepository.CheckIfExistsAsync(request.StudentId))
            throw new NotFound("Student does not exist");

        var student = await _studentRepository.GetByIdAsync(request.StudentId);

        if (await _selectionRepository.CheckIfStudentHasSelectionAsync(request.StudentId))
            throw new BadRequest("Student already has a selection");

        var selection = new SelectionEntity
        {
            DeadLine = request.Deadline,
            SelectionStatus = SelectionStatus.Inactive,
            CandidateId = default,
            Candidate = null
        };

        var candidate = await _mediator.Send(new CreateCandidateCommand(student.UserId, student.Id, selection),
            cancellationToken);

        selection.CandidateId = candidate.Id;
        selection.Candidate = candidate;

        await _selectionRepository.AddAsync(selection);

        return Unit.Value;
    }
}