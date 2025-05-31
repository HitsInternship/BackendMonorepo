using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Commands;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Entities;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class EditStudentCommandHandler : IRequestHandler<EditStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMediator _mediator;

        public EditStudentCommandHandler(IStudentRepository studentRepository, IMediator mediator)
        {
            _studentRepository = studentRepository;
            _mediator = mediator;
        }

        public async Task<StudentDto> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.studentId)
                ?? throw new NotFound("Student not found");

            var userRequest = new UserRequest()
            {
                name = request.name,
                surname = request.surnamename,
                email = request.email
            };

            var editUserCommand = new EditUserCommand(student.UserId, userRequest);
            var user = await _mediator.Send(editUserCommand);

            student.Middlename = request.middlename;
            student.Phone = request.phone;
            student.IsHeadMan = request.isHeadMan;
            student.User = user;

            return new StudentDto(student);
        }
    }
}
