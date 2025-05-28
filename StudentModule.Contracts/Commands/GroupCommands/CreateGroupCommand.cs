using MediatR;
using StudentModule.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Commands.GroupCommands
{
    public record CreateGroupCommand() : IRequest<GroupDto>
    {
        public int GroupNumber { get; set; }
        public Guid StreamId { get; set; }
    }
}
