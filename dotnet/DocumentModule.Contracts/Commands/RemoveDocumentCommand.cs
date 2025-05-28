using DocumentModule.Domain.Enums;
using MediatR;

namespace DocumentModule.Contracts.Commands
{
    public record RemoveDocumentCommand(Guid documentId, DocumentType documentType) : IRequest<Unit>;
}
