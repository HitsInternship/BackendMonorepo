using MediatR;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace PracticeModule.Application.Handler.Practice;

public class GetStudentPracticeQueryHandler : IRequestHandler<GetStudentPracticeQuery, Domain.Entity.Practice>
{
    private readonly IPracticeRepository _practiceRepository;

    public GetStudentPracticeQueryHandler(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<Domain.Entity.Practice> Handle(GetStudentPracticeQuery query, CancellationToken cancellationToken)
    {
        Domain.Entity.Practice? practice =
            (await _practiceRepository.ListAllActiveAsync()).FirstOrDefault(practice =>
                practice.StudentId == query.studentId);
        if (practice == null)
        {
            throw new NotFound("No practice for this student");
        }

        return practice;
    }
}