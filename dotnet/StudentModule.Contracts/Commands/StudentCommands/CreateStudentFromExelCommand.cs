using AuthModule.Contracts.Model;
using MediatR;
using StudentModule.Contracts.DTOs;

namespace StudentModule.Contracts.Commands.StudentCommands
{
    public record  CreateStudentFromExelCommand : IRequest<StudentDto>
    {
        public ExcelStudentDTO ExelStudentDto { get; set; }
    }
}
