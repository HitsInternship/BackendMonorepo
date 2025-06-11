using DocumentModule.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentModule.Contracts.Repositories
{
    public interface IFileRepository
    {
        Task<string> AddFileAsync(Guid fileId, DocumentType documentType, IFormFile file);
        Task<string> GetFileNameAsync(Guid fileId, DocumentType documentType);
        Task<FileContentResult> GetFileAsync(Guid fileId, DocumentType documentType);
        Task DeleteFileAsync(Guid fileId, DocumentType documentType);
    }
}
