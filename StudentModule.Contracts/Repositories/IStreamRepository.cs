using Shared.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Repositories
{
    public interface IStreamRepository : IBaseEntityRepository<StreamEntity>
    {
        Task<bool> IsStreamWithNumderExistsAsync(int number);
        Task<List<StreamEntity>> GetStreamsAsync();
        Task<StreamEntity> GetStreamByIdAsync(Guid id);
    }
}
