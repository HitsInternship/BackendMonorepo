using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StudentQueries;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class GetStudentHimselfQueryHandler : IRequestHandler<GetStudentHimselfQuery, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public GetStudentHimselfQueryHandler(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public async Task<StudentDto> Handle(GetStudentHimselfQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId)
                ?? throw new NotFound("User not found");

            var student = await _studentRepository.GetStudentByUserIdAsync(request.userId);

            student.User = user;
            return new StudentDto(student);
        }
    }
}
