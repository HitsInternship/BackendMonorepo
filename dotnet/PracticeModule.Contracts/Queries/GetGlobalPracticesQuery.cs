using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Contracts.Queries
{
    public record GetGlobalPracticesQuery(Guid? studentUserId = null) : IRequest<List<IGrouping<SemesterEntity, GlobalPractice>>>;
}
