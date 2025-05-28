using DocumentModule.Contracts.Queries;
using DocumentModule.Contracts.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentModule.Application.Handlers
{
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, FileContentResult>
    {
        private readonly IFileRepository _fileRepository;
        public GetDocumentQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileContentResult> Handle(GetDocumentQuery query, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetFileAsync(query.documentId, query.documentType);
        }
    }
}
