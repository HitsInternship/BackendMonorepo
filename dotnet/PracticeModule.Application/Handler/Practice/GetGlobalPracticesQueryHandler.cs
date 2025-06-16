using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace PracticeModule.Application.Handler.Practice;

public class GetGlobalPracticesQueryHandler : IRequestHandler<GetGlobalPracticesQuery, List<GlobalPractice>>
{
    private readonly IGlobalPracticeRepository _globalPracticeRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStreamRepository _streamRepository;

    public GetGlobalPracticesQueryHandler(IGlobalPracticeRepository globalPracticeRepository, ISemesterRepository semesterRepository, IStreamRepository streamRepository)
    {
        _globalPracticeRepository = globalPracticeRepository;
        _semesterRepository = semesterRepository;
        _streamRepository = streamRepository;
    }

    public async Task<List<GlobalPractice>> Handle(GetGlobalPracticesQuery query, CancellationToken cancellationToken)
    {
        List<GlobalPractice> globalPractices = (await _globalPracticeRepository.ListAllAsync()).ToList();

        List<SemesterEntity> semesters = (await _semesterRepository.ListAllAsync()).Where(semester => globalPractices.Select(globalPractice => globalPractice.SemesterId).Contains(semester.Id)).ToList();
        List<StreamEntity> streams = (await _streamRepository.ListAllAsync()).Where(stream => globalPractices.Select(globalPractice => globalPractice.StreamId).Contains(stream.Id)).ToList();

        globalPractices.ForEach(globalPractice => {
            globalPractice.Semester = semesters.FirstOrDefault(semester => semester.Id == globalPractice.SemesterId);
            globalPractice.Stream = streams.FirstOrDefault(stream => stream.Id == globalPractice.StreamId);
        });

        return globalPractices.OrderByDescending(globalPractice => globalPractice.Semester.EndDate).ToList();
    }
}