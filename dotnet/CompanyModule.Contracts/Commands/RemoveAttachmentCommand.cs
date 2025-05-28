using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Commands
{
    public record RemoveAttachmentCommand(Guid attachmentId) : IRequest<Unit>;
}
