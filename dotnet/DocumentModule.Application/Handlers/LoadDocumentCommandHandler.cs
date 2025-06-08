using DocumentModule.Contracts.Commands;
using DocumentModule.Contracts.Repositories;
using MediatR;

namespace DocumentModule.Application.Handlers
{
    public class LoadDocumentCommandHandler : IRequestHandler<LoadDocumentCommand, Guid>
    {
        private readonly IFileRepository _fileRepository;

        public LoadDocumentCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Guid> Handle(LoadDocumentCommand command, CancellationToken cancellationToken)
        {
            Guid fileId = Guid.NewGuid();

            await _fileRepository.AddFileAsync(command.FileId ?? fileId, command.DocumentType, command.File,
                command.FileName ?? string.Empty);

            return fileId;
        }
    }
}