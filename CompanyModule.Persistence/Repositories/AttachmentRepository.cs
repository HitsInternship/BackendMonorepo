using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using CompanyModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace CompanyModule.Persistence.Repositories
{
    public class AttachmentRepository : BaseEntityRepository<Attachment>, IAttachmentRepository
    {
        private readonly CompanyModuleDbContext context;

        public AttachmentRepository(CompanyModuleDbContext context) : base(context)
        {
            this.context = context;
        }
    }

}
