using MediatR;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Commands.StudentCommands
{
    public record EditStudentStatusCommand : IRequest<StudentDto>
    {
        public Guid StudentId { get; set; }
        public StudentStatus Status { get; set; }
    }
}
