using DeanModule.Domain.Entities;
using Shared.Contracts.Repositories;

namespace DeanModule.Contracts.Repositories;

public interface IStreamSemesterRepository : IBaseEntityRepository<StreamSemesterEntity>
{
    Task<IEnumerable<StreamSemesterEntity>> GetByStreamIdAsync(Guid streamId);
    Task<StreamSemesterEntity?> GetBySemesterIdAsync(Guid semesterId, int semesterNumber);
    new Task<IQueryable<StreamSemesterEntity>> ListAllAsync();
    Task SoftDeleteRangBySemesterAsync(Guid semesterId);
    Task SoftDeleteRangeByStreamAsync (Guid streamId);
}