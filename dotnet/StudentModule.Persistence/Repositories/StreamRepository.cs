using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;
using StudentModule.Infrastructure;
using System.IO;

namespace StudentModule.Persistence.Repositories
{
    public class StreamRepository : BaseEntityRepository<StreamEntity>, IStreamRepository
    {
        private readonly StudentModuleDbContext context;
        public StreamRepository(StudentModuleDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> IsStreamWithNumderExistsAsync(int number)
        {
            return await context.Streams.AnyAsync(s => s.StreamNumber == number);
        }

        public async Task<List<StreamEntity>> GetStreamsAsync()
        {
            var streams = await context.Streams.Include(s => s.Groups).ThenInclude(g => g.Students).ToListAsync();
            
            return streams;
        }

        public async Task<StreamEntity> GetStreamByIdAsync(Guid id)
        {
            var stream = await context.Streams.Include(s => s.Groups).ThenInclude(g => g.Students).FirstOrDefaultAsync(s => s.Id == id);

            return stream;
        }
    }
}
