using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Practice>> GetPracticesByStudentIdAsync(List<Guid> studentsId, Guid semesterId)
        {
            return context.Practice.Include(p => p.GlobalPractice).Where(practice => studentsId.Contains(practice.StudentId) && practice.GlobalPractice.SemesterId == semesterId).ToList();
        }
    }
}
