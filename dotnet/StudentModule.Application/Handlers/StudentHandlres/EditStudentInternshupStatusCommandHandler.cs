using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class EditStudentInternshipStatusCommandHandler : IRequestHandler<EditStudentInternshipStatusCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public EditStudentInternshipStatusCommandHandler(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }
        public async Task<StudentDto> Handle(EditStudentInternshipStatusCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id)
                ?? throw new NotFound("Student not found");

            var user = await _userRepository.GetByIdAsync(student.UserId);


            student.InternshipStatus = request.Status;
            await _studentRepository.UpdateAsync(student);

            student.User = user;
            return new StudentDto(student);
        }
    }
}
