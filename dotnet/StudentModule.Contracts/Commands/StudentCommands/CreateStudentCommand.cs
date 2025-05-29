using MediatR;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.DTOs.Requests;

namespace StudentModule.Contracts.Commands.StudentCommands
{
    public record CreateStudentCommand : IRequest<StudentDto>
    {
        public UserRequest? userRequest { get; set; }
        public Guid? userId { get; set; }
        public string? Middlename { get; set; }
        public string Phone { get; set; }
        public bool IsHeadMan { get; set; }
        public StudentStatus Status { get; set; }
        public StudentInternshipStatus InternshipStatus { get; set; }
        public Guid GroupId { get; set; }
    }
}
