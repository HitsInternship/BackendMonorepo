using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Infrastructure;
using UserModule.Infrastructure;

namespace StudentModule.Persistence.Repositories
{
    public class StudentRepository(StudentModuleDbContext context, UserModuleDbContext userDbContext)
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

        public async Task<List<Guid>> GetIdsByName(string name)
        {
            var lowerName = name.ToLower();

            var users = await userDbContext.Users
                .Select(u => new { u.Id, u.Name, u.Surname })
                .ToListAsync();

            var students = await context.SStudents
                .Select(s => new { s.Id, s.UserId, s.Middlename })
                .ToListAsync();

            var result = (from s in students
                    join u in users on s.UserId equals u.Id
                    let fullName = (u.Surname + " " + u.Name + " " + (s.Middlename ?? "")).Trim()
                    where fullName.ToLower().Contains(lowerName)
                    select s.Id)
                .ToList();

            return result;
        }
    }
}