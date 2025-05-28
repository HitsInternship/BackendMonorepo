using MediatR;
using StudentModule.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Commands.StudentCommands
{
    public record EditStudentGroupCommand : IRequest<StudentDto>
    {
        public Guid studentId { get; set; }
        public Guid groupId { get; set; }
    }
}
