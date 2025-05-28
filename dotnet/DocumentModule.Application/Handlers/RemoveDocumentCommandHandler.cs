using DocumentModule.Contracts.Commands;
using DocumentModule.Contracts.Queries;
using DocumentModule.Contracts.Repositories;
using MediatR;

namespace DocumentModule.Application.Handlers
{
    public class RemoveDocumentCommandHandler : IRequestHandler<RemoveDocumentCommand, Unit>
    {
        private readonly IFileRepository _fileRepository;
        public RemoveDocumentCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(RemoveDocumentCommand command, CancellationToken cancellationToken)
        {
            await _fileRepository.DeleteFileAsync(command.documentId, command.documentType);

            return Unit.Value;
        }
    }
}
