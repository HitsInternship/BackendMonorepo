using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using CompanyModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace CompanyModule.Persistence.Repositories
{
    public class AppointmentRepository : BaseEntityRepository<Appointment>, IAppointmentRepository
    {
        private readonly CompanyModuleDbContext context;

        public AppointmentRepository(CompanyModuleDbContext context) : base(context)
        {
            this.context = context;
        }
    }

}
