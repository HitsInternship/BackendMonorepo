using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentModule.Contracts.Queries
{
    public record GetDocumentQuery(Guid documentId, DocumentType documentType) : IRequest<FileContentResult>;
}
