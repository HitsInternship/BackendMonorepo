using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DocumentModule.Contracts.Commands
{
    public record LoadDocumentCommand(
        DocumentType DocumentType,
        IFormFile File,
        string? FileName = null,
        Guid? FileId = null) : IRequest<Guid>;
}