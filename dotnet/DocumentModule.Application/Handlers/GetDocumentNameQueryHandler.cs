using DocumentModule.Contracts.Queries;
using DocumentModule.Contracts.Repositories;
using MediatR;

namespace DocumentModule.Application.Handlers
{
    public class GetDocumentNameQueryHandler : IRequestHandler<GetDocumentNameQuery, string>
    {
        private readonly IFileRepository _fileRepository;
        public GetDocumentNameQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string> Handle(GetDocumentNameQuery query, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetFileNameAsync(query.documentId, query.documentType);
        }
    }
}
