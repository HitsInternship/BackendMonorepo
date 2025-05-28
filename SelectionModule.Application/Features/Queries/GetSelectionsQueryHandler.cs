using MediatR;
using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace SelectionModule.Application.Features.Queries;

public class GetSelectionsQueryHandler : IRequestHandler<GetSelectionsQuery, List<ListedSelectionDto>>
{
    private readonly ISelectionRepository _selectionRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IStudentRepository _studentRepository;

    public GetSelectionsQueryHandler(ISelectionRepository selectionRepository, ICandidateRepository candidateRepository,
        IStudentRepository studentRepository)
    {
        _selectionRepository = selectionRepository;
        _candidateRepository = candidateRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<ListedSelectionDto>> Handle(GetSelectionsQuery request, CancellationToken cancellationToken)
    {
        List<Guid> userIds;
        IQueryable<StudentEntity> students = await _studentRepository.ListAllAsync();
        var selectionsEntity = await _selectionRepository.ListAllAsync();

        if (request.GroupNumber.HasValue)
        {
            students = students.Where(x => x.Group.GroupNumber == request.GroupNumber.Value);
            userIds = students.Select(s => s.UserId).ToList();
            selectionsEntity = selectionsEntity.Where(x => userIds.Contains(x.Id));
        }

        if (request.Status.HasValue)
        {
            selectionsEntity = selectionsEntity.Where(x => x.SelectionStatus == request.Status.Value);
        }

        if (request.IsArchive.HasValue)
        {
            selectionsEntity = selectionsEntity.Where(x => x.IsDeleted == false);
        }

        var selections = new List<ListedSelectionDto>();

        foreach (var selectionEntity in selectionsEntity)
        {
            var candidate = await _candidateRepository.GetByIdAsync(selectionEntity.CandidateId);
            var student = await students.FirstOrDefaultAsync(x => x.Id == candidate.StudentId,
                cancellationToken);

            selections.Add(new ListedSelectionDto
            {
                Id = selectionEntity.Id,
                IsDeleted = selectionEntity.IsDeleted,
                SelectionStatus = selectionEntity.SelectionStatus,
                Candidate = new CandidateDto
                {
                    Id = candidate.Id,
                    IsDeleted = candidate.IsDeleted,
                    Name = student.User.Name,
                    Surname = student.User.Surname,
                    Middlename = student.Middlename,
                    Email = student.User.Email,
                    Phone = student.Phone,
                    GroupNumber = student.Group.GroupNumber,
                }
            });
        }

        return selections;
    }
}