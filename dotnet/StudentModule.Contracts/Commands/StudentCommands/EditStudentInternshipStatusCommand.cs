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
    public record EditStudentInternshipStatusCommand : IRequest<StudentDto>
    {
        public Guid Id { get; set; }
        public StudentInternshipStatus Status { get; set; }
    }
}
