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
    public class PracticeDiaryCommentRepository : BaseEntityRepository<PracticeDiaryComment>, IPracticeDiaryCommentRepository
    {
        private readonly PracticeDbContext context;

        public PracticeDiaryCommentRepository(PracticeDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
