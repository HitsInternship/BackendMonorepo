using AuthModule.Contracts.Model;
using MediatR;
using StudentModule.Contracts.DTOs;

namespace StudentModule.Contracts.Commands.StudentCommands
{
    public record  CreateStudentFromExelCommand : IRequest<List<StudentDto>>
    {
        public List<ExcelStudentDTO> ExelStudentDto { get; set; }
    }
}
