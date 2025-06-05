using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Infrastructure;

namespace StudentModule.Persistence.Repositories
{
    public class GroupRepository : BaseEntityRepository<GroupEntity>, IGroupRepository
    {
        private readonly StudentModuleDbContext context;
        public GroupRepository(StudentModuleDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<GroupEntity>> GetGroupAsync()
        {
            var groups = context.Groups
                .Include(g => g.Stream)
                .Include(g => g.Students)
                .ToList();

            return groups;
        }

        public async Task<GroupEntity> GetGroupByIdAsync(Guid id)
        {
            var group = await context.Groups
                        .Include(g => g.Stream)
                        .Include(g => g.Students)
                        .FirstOrDefaultAsync(g => g.Id == id);

            return group;
        }

        public async Task<GroupEntity> GetGroupByNumberAsync(int number)
        {
            var group = await context.Groups
                .Include(g => g.Stream)
                .FirstOrDefaultAsync(g => g.GroupNumber == number);

            return group;
        }

        public async Task<bool> IsGroupWithNumderExistsAsync(int number)
        {
            return await context.Groups.AnyAsync(s => s.GroupNumber == number);
        }
    }
}
