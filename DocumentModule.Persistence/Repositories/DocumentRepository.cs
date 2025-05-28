using DocumentModule.Domain.Entities;
using DocumentModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace DocumentModule.Persistence.Repositories
{
    public class DocumentRepository : BaseEntityRepository<Document>
    {
        public DocumentRepository(DocumentModuleDbContext context) : base(context) { }
    }
}
