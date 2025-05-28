using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Commands.GroupCommands
{
    public record DeleteGroupCommand : IRequest<Unit>
    {
        public Guid GroupId { get; set; }
    }
}
