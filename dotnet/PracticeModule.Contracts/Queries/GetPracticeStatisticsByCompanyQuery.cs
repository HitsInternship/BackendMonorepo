using CompanyModule.Domain.Entities;
using DeanModule.Domain.Entities;
using MediatR;

namespace PracticeModule.Contracts.Queries
{
    public record GetPracticeStatisticsByCompanyQuery(List<Guid> companyIds) : IRequest<Dictionary<SemesterEntity, Dictionary<Company, int>>>;
}
