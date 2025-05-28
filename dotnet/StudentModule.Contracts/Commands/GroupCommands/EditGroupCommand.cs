using MediatR;
using StudentModule.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Commands.GroupCommands
{
    public record EditGroupCommand() : IRequest<GroupDto>
    {
        public Guid Id { get; set; }
        public int GroupNumber { get; set; }
    }
}
