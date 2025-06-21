using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace PracticeModule.Application.Handler.PracticePart;

public class GetStudentPracticeQueryHandler : IRequestHandler<GetStudentPracticeQuery, Practice>
{
    private readonly IPracticeRepository _practiceRepository;

    public GetStudentPracticeQueryHandler(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<Practice> Handle(GetStudentPracticeQuery query, CancellationToken cancellationToken)
    {
        Practice? practice =
            (await _practiceRepository.ListAllActiveAsync()).FirstOrDefault(practice =>
                practice.StudentId == query.studentId);
        if (practice == null)
        {
            throw new NotFound("No practice for this student");
        }

        return practice;
    }
}