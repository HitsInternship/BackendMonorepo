using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, Attachment>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        public GetAttachmentQueryHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<Attachment> Handle(GetAttachmentQuery query, CancellationToken cancellationToken)
        {
            return (await _attachmentRepository.ListAllAsync()).Where(attachment => attachment.DocumentId == query.attachmentId).Include(attachment => attachment.Appointment.Company).FirstOrDefault();
        }
    }
}
