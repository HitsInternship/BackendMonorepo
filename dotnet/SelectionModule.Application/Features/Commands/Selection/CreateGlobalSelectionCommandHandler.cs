using System.Runtime.Intrinsics.X86;
using DeanModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace SelectionModule.Application.Features.Commands.Selection;

public class CreateGlobalSelectionCommandHandler : IRequestHandler<CreateGlobalSelectionCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IGlobalSelectionRepository _globalSelectionRepository;
    private readonly IStreamRepository _streamRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStudentRepository _studentRepository;


    public CreateGlobalSelectionCommandHandler(ISender mediator, IGlobalSelectionRepository globalSelectionRepository,
        IStreamRepository streamRepository, ISemesterRepository semesterRepository,
        IStudentRepository studentRepository)
    {
        _mediator = mediator;
        _globalSelectionRepository = globalSelectionRepository;
        _streamRepository = streamRepository;
        _semesterRepository = semesterRepository;
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(CreateGlobalSelectionCommand request, CancellationToken cancellationToken)
    {
        if (!await _semesterRepository.CheckIfExistsAsync(request.SelectionRequestDto.SemesterId))
            throw new BadRequest("Invalid semester");

        if (!await _streamRepository.CheckIfExistsAsync(request.SelectionRequestDto.StreamId))
            throw new BadRequest("Invalid stream");

        var stream = await _streamRepository.GetStreamByIdAsync(request.SelectionRequestDto.StreamId);

        var semester = await _semesterRepository.GetByIdAsync(request.SelectionRequestDto.SemesterId);

        if((await _globalSelectionRepository.FindAsync(x=>x.IsDeleted == false && x.StreamId == stream.Id)).Any())
            throw new BadRequest("Selection for selected stream already exists");
        
        var selection = new GlobalSelection
        {
            StreamId = stream.Id,
            SemesterId = semester.Id,
            StartDate = semester.StartDate,
            EndDate = request.SelectionRequestDto.Deadline ?? semester.EndDate,
            Selections = null
        };


        List<StudentEntity> students = new List<StudentEntity>();

        foreach (var group in stream.Groups)
        {
            students.AddRange(await _studentRepository.GetStudentsByGroup(group.GroupNumber));
        }
        
        if (students.Count == 0) throw new BadRequest("No students found");

        await _globalSelectionRepository.AddAsync(selection);

        await _mediator.Send(new CreateSelectionCommand(selection, students), cancellationToken);

        return Unit.Value;
    }
}