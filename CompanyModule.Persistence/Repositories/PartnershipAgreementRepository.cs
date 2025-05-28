using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using CompanyModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace CompanyModule.Persistence.Repositories
{
    public class PartnershipAgreementRepository : BaseEntityRepository<PartnershipAgreement>, IPartnershipAgreementRepository
    {
        private readonly CompanyModuleDbContext context;

        public PartnershipAgreementRepository(CompanyModuleDbContext context) : base(context)
        {
            this.context = context;
        }
    }

}
