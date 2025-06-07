using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using CompanyModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace CompanyModule.Persistence.Repositories
{
    public class TimeslotRepository : BaseEntityRepository<Timeslot>, ITimeslotRepository
    {
        private readonly CompanyModuleDbContext context;

        public TimeslotRepository(CompanyModuleDbContext context) : base(context)
        {
            this.context = context;
        }
    }

}
