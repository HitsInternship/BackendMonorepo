using DeanModule.Domain.Entities;
using MediatR;
using SelectionModule.Domain.Entites;

namespace PracticeModule.Contracts.Queries
{
    public record GetPracticeStatisticsByPositionQuery(List<Guid> positionIds, List<Guid>? companyIds) : IRequest<Dictionary<SemesterEntity, Dictionary<PositionEntity, int>>>;
}
