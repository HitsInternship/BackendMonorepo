using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DocumentModule.Contracts.Repositories;
using DocumentModule.Domain.Enums;
using MediatR;

namespace CompanyModule.Application.Handlers.AppointmentPart
{
    public class AddAttachmentCommandHandler : IRequestHandler<AddAttachmentCommand, Guid>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileRepository _fileRepository;
        public AddAttachmentCommandHandler(IAppointmentRepository appointmentRepository, IAttachmentRepository attachmentRepository, IFileRepository fileRepository)
        {
            _appointmentRepository = appointmentRepository;
            _attachmentRepository = attachmentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Guid> Handle(AddAttachmentCommand command, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepository.GetByIdAsync(command.appointmentId);

            Attachment attachment = new Attachment() { DocumentId = Guid.NewGuid(), Appointment = appointment };
            await _attachmentRepository.AddAsync(attachment);
            await _fileRepository.AddFileAsync(attachment.DocumentId, DocumentType.Attachement, command.attachment);

            return attachment.DocumentId;
        }
    }
}
