using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Infrastructure;

namespace StudentModule.Persistence.Repositories
{
    public class StudentRepository(StudentModuleDbContext context)
        : BaseEntityRepository<StudentEntity>(context), IStudentRepository
    {
        public async Task<List<StudentEntity>> GetStudentsByGroup(int groupNumber)
        {
            return await DbSet.Where(x => x.Group.GroupNumber == groupNumber).AsNoTracking().ToListAsync();
        }

        public async Task<StudentEntity> GetStudentByIdAsync(Guid id)
        {
            var student = await context.SStudents
                .Include(s => s.Group)
                .ThenInclude(g => g.Stream)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }

        public async Task<StudentEntity> GetStudentByUserIdAsync(Guid id)
        {
            var student = await context.SStudents
                .Include(s => s.Group)
                .ThenInclude(g => g.Stream)
                .FirstOrDefaultAsync(s => s.UserId == id);

            return student;
        }

        public async Task<StudentEntity> GetStudentByName(Guid userId, string? middlename)
        {
            var students = await context.SStudents
                .Include(s => s.Group)
                .ThenInclude(g => g.Stream)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Middlename == middlename);

            return students;
        }

        public async Task<List<StudentEntity>> GetStudentsByGroupIdAsync(Guid groupId)
        {
            var students = await context.SStudents
                .Where(s => s.GroupId == groupId)
                .ToListAsync();

            return students;
        }
    }
}