using DocumentModule.Domain.Enums;
using MediatR;

namespace DocumentModule.Contracts.Queries
{
    public record GetDocumentNameQuery(Guid documentId, DocumentType documentType) : IRequest<string>;
}
