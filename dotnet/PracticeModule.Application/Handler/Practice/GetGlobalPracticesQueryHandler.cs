using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace PracticeModule.Application.Handler.PracticePart;

public class GetGlobalPracticesQueryHandler : IRequestHandler<GetGlobalPracticesQuery, List<IGrouping<SemesterEntity, GlobalPractice>>>
{
    private readonly IGlobalPracticeRepository _globalPracticeRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStreamRepository _streamRepository;

    public GetGlobalPracticesQueryHandler(IGlobalPracticeRepository globalPracticeRepository, IStudentRepository studentRepository, ISemesterRepository semesterRepository, IStreamRepository streamRepository)
    {
        _globalPracticeRepository = globalPracticeRepository;
        _studentRepository = studentRepository;
        _semesterRepository = semesterRepository;
        _streamRepository = streamRepository;
    }

    public async Task<List<IGrouping<SemesterEntity, GlobalPractice>>> Handle(GetGlobalPracticesQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = (await _globalPracticeRepository.ListAllAsync());
        if (query.studentUserId != null)
        {
            Guid studentId = (await _studentRepository.ListAllAsync()).Where(student => student.UserId == query.studentUserId).Select(student => student.Id).First();
            dbQuery = dbQuery
                .Where(globalPractice => globalPractice.Practices.Any(practice => practice.StudentId == studentId))
                .Include(globalPractice => globalPractice.Practices.Where(practice => practice.StudentId == studentId))
                    .ThenInclude(practice => practice.StudentPracticeCharacteristics)
                .Include(globalPractice => globalPractice.Practices.Where(practice => practice.StudentId == studentId))
                    .ThenInclude(practice => practice.PracticeDiary);
        }
        List<GlobalPractice> globalPractices = dbQuery.ToList();
        List<SemesterEntity> semesters = (await _semesterRepository.ListAllAsync()).Where(semester => globalPractices.Select(globalPractice => globalPractice.SemesterId).Contains(semester.Id)).ToList();
        List<StreamEntity> streams = (await _streamRepository.ListAllAsync()).Where(stream => globalPractices.Select(globalPractice => globalPractice.StreamId).Contains(stream.Id)).ToList();

        globalPractices.ForEach(globalPractice => {
            globalPractice.Semester = semesters.FirstOrDefault(semester => semester.Id == globalPractice.SemesterId);
            globalPractice.Stream = streams.FirstOrDefault(stream => stream.Id == globalPractice.StreamId);
        });

        return globalPractices.GroupBy(globalPractice => globalPractice.Semester).OrderByDescending(group => group.Key.EndDate).ToList();
    }
}