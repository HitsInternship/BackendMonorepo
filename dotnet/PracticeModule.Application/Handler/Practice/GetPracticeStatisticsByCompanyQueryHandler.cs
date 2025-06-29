using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;

namespace PracticeModule.Application.Handler.Practice;

public class GetPracticeStatisticsByCompanyQueryHandler : IRequestHandler<GetPracticeStatisticsByCompanyQuery, Dictionary<SemesterEntity, Dictionary<Company, int>>>
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly ICompanyRepository _companyRepository;

    public GetPracticeStatisticsByCompanyQueryHandler(IPracticeRepository practiceRepository, ISemesterRepository semesterRepository, ICompanyRepository companyRepository)
    {
        _practiceRepository = practiceRepository;
        _companyRepository = companyRepository;
        _semesterRepository = semesterRepository;
    }

    public async Task<Dictionary<SemesterEntity, Dictionary<Company, int>>> Handle(GetPracticeStatisticsByCompanyQuery query, CancellationToken cancellationToken)
    {
        Dictionary<SemesterEntity, Dictionary<Company, int>> statistics = new Dictionary<SemesterEntity, Dictionary<Company, int>>();

        List<SemesterEntity> semestersOrdered = (await _semesterRepository.ListAllAsync()).OrderBy(semester => semester.EndDate).ToList();
        List<Company> companies = (await _companyRepository.ListAllAsync()).Where(company => query.companyIds.Contains(company.Id)).ToList();

        foreach (var semester in semestersOrdered)
        {
            Dictionary<Company, int> companiesStatsInSemester = (await _practiceRepository.ListAllAsync()).Where(practice => practice.GlobalPractice.SemesterId == semester.Id && query.companyIds.Contains(practice.CompanyId)).GroupBy(practice => practice.CompanyId).ToDictionary(group => companies.First(company => company.Id == group.Key), group => group.Count());
            statistics.Add(semester, companiesStatsInSemester);
        }

        return statistics;
    }
}