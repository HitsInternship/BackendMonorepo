using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Commands
{
    public record AddAttachmentCommand(Guid appointmentId, IFormFile attachment) : IRequest<Guid>;
}
