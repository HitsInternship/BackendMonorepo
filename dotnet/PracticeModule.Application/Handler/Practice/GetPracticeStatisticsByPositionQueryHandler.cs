using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;


namespace PracticeModule.Application.Handler.PracticePart;

public class GetPracticeStatisticsByPositionQueryHandler : IRequestHandler<GetPracticeStatisticsByPositionQuery, Dictionary<SemesterEntity, Dictionary<PositionEntity, int>>>
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IPositionRepository _positionRepository;

    public GetPracticeStatisticsByPositionQueryHandler(IPracticeRepository practiceRepository, ISemesterRepository semesterRepository, IPositionRepository positionRepository)
    {
        _practiceRepository = practiceRepository;
        _positionRepository = positionRepository;
        _semesterRepository = semesterRepository;
    }

    public async Task<Dictionary<SemesterEntity, Dictionary<PositionEntity, int>>> Handle(GetPracticeStatisticsByPositionQuery query, CancellationToken cancellationToken)
    {
        Dictionary<SemesterEntity, Dictionary<PositionEntity, int>> statistics = new Dictionary<SemesterEntity, Dictionary<PositionEntity, int>>();

        List<SemesterEntity> semestersOrdered = (await _semesterRepository.ListAllAsync()).OrderBy(semester => semester.EndDate).ToList();
        List<PositionEntity> positions = (await _positionRepository.ListAllAsync()).Where(position => query.positionIds.Contains(position.Id)).ToList();

        foreach (var semester in semestersOrdered)
        {
            var dbQuery = (await _practiceRepository.ListAllAsync()).Where(practice => practice.GlobalPractice.SemesterId == semester.Id && query.positionIds.Contains(practice.PositionId));
            if (query.companyIds.Count > 0)
            {
                dbQuery = dbQuery.Where(practice => query.companyIds.Contains(practice.CompanyId));
            }
            Dictionary<PositionEntity, int> positionStatsInSemester = dbQuery.GroupBy(practice => practice.PositionId).ToDictionary(group => positions.First(position => position.Id == group.Key), group => group.Count());
            statistics.Add(semester, positionStatsInSemester);
        }

        return statistics;
    }
}