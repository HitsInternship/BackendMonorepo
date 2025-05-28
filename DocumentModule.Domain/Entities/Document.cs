using DocumentModule.Domain.Enums;
using Shared.Domain.Entites;

namespace DocumentModule.Domain.Entities
{
    public class Document : BaseEntity
    {
        public DocumentType DocumentType { get; set; }
        public Document() { }
    }
}
