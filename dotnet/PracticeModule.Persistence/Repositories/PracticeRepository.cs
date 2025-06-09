using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using PracticeModule.Infrastructure;
using Shared.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Persistence.Repositories
{
    public class PracticeRepository : BaseEntityRepository<Practice>, IPracticeRepository
    {
        private readonly PracticeDbContext context;

        public PracticeRepository(PracticeDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
