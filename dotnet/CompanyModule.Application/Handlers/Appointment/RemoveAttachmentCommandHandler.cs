using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DocumentModule.Contracts.Repositories;
using DocumentModule.Domain.Enums;
using MediatR;

namespace CompanyModule.Application.Handlers.AppointmentPart
{
    public class RemoveAttachmentCommandHandler : IRequestHandler<RemoveAttachmentCommand, Unit>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileRepository _fileRepository;
        public RemoveAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IFileRepository fileRepository)
        {
            _attachmentRepository = attachmentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(RemoveAttachmentCommand command, CancellationToken cancellationToken)
        {
            Attachment attachment = (await _attachmentRepository.ListAllAsync()).First(attachment => attachment.DocumentId == command.attachmentId);
            await _attachmentRepository.DeleteAsync(attachment);
            await _fileRepository.DeleteFileAsync(attachment.DocumentId, DocumentType.Attachement);

            return Unit.Value;
        }
    }
}
