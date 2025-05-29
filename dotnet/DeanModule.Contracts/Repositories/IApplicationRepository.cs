using DeanModule.Domain.Entities;
using DeanModule.Domain.Enums;
using Shared.Contracts.Repositories;

namespace DeanModule.Contracts.Repositories;

public interface IApplicationRepository : IBaseEntityRepository<ApplicationEntity>
{
    Task<IEnumerable<ApplicationEntity>> GetByStudentIdAsync(Guid studentId);
    Task<IEnumerable<ApplicationEntity>> GetByCompanyId(Guid companyId);
    Task<IEnumerable<ApplicationEntity>> GetByPositionIdAsync(Guid positionId);
    Task<IEnumerable<ApplicationEntity>> GetByStatusAsync(ApplicationStatus status);
    
    new Task<ApplicationEntity> GetByIdAsync(Guid id);
}