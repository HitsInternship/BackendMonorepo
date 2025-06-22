using MediatR;
using Shared.Extensions.Validation;
using StudentModule.Contracts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StudentModule.Contracts.Commands.StudentCommands
{
    public record EditStudentCommand : IRequest<StudentDto>
    {
        public Guid studentId { get; set; }
        public string name { get; set; }
        public string surnamename { get; set; }
        public string middlename { get; set; }

        [DataType(DataType.EmailAddress)]
        [Annotations.Email]
        public string email { get; set; }
        public string phone { get; set; }
        public bool isHeadMan { get; set; }
    }
}

