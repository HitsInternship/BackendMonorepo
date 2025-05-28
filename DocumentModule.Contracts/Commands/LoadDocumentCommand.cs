using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DocumentModule.Contracts.Commands
{
    public record LoadDocumentCommand(DocumentType documentType, IFormFile file) : IRequest<Guid>;
}
