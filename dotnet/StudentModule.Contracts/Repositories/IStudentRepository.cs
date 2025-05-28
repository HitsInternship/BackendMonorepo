using Shared.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace StudentModule.Contracts.Repositories
{
    public interface IStudentRepository : IBaseEntityRepository<StudentEntity>
    {
        Task<List<StudentEntity>> GetStudentsByGroup(int groupNumber);
        Task<StudentEntity> GetStudentByIdAsync(Guid id);
        Task<StudentEntity> GetStudentByUserIdAsync(Guid id);
    }
}
